using AutoMapper;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ECommerce.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IBlobStorageHelper _fileUploadHelper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IRedisCacheService redisCacheService, IBlobStorageHelper fileUploadHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _fileUploadHelper = fileUploadHelper;
        }

        public async Task<GetProductDto> GetProductById(string id)
        {
            var cacheKey = $"product:{id}";
            var cachedProduct = await _redisCacheService.GetCachedDataAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProduct))
            {
                return JsonConvert.DeserializeObject<GetProductDto>(cachedProduct);
            }

            var product = await _unitOfWork.GetRepository<Product>().Entities
                .Include(p => p.ProductInventories)
                    .ThenInclude(pi => pi.InventoryVariations)
                        .ThenInclude(iv => iv.ProductVariation)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This product is not found!");

            if (product != null)
            {
                foreach (var inventory in product.ProductInventories)
                {
                    inventory.InventoryVariations = inventory.InventoryVariations
                        .OrderBy(iv => iv.ProductVariation.Type)
                        .ToList();
                }
            }

            // Calculate DiscountedPrice and OriginalPrice for each inventory
            var productDto = _mapper.Map<GetProductDto>(product);

            foreach (var inventory in productDto.ProductInventories)
            {
                var productInventory = product.ProductInventories
                    .First(pi => pi.Id == inventory.Id); // Find the corresponding ProductInventory entity

                // Set the OriginalPrice and DiscountedPrice
                var discountedPrice = await GetDiscountedPrice(productDto.Id, inventory.Id);
                inventory.OriginalPrice = productInventory.Price; // Original price comes from the entity
                inventory.DiscountPrice = discountedPrice; // Discounted price calculated separately
            }

            // Cache the product with the discount and original price
            await _redisCacheService.SetCachedDataAsync(cacheKey, JsonConvert.SerializeObject(productDto), TimeSpan.FromMinutes(30));

            return productDto;
        }


        public async Task<IEnumerable<GetProductDto>> GetAllProducts()
        {
            const string cacheKey = "all_products";
            var cachedProducts = await _redisCacheService.GetCachedDataAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return JsonConvert.DeserializeObject<IEnumerable<GetProductDto>>(cachedProducts);
            }

            var products = await _unitOfWork.GetRepository<Product>().Entities
                .Include(p => p.ProductInventories)
                .Include(p => p.ProductImages)
                .ToListAsync();

            var productDtos = _mapper.Map<IEnumerable<GetProductDto>>(products);
            await _redisCacheService.SetCachedDataAsync(cacheKey, JsonConvert.SerializeObject(productDtos), TimeSpan.FromMinutes(30));

            return productDtos;
        }

        public async Task<IEnumerable<GetProductDto>> GetProduct(int index, int pageSize)
        {
            var products = await _unitOfWork.GetRepository<Product>()
                .GetPaginationListAsync(
                    _unitOfWork.GetRepository<Product>().Entities
                        .Include(p => p.ProductInventories)
                        .Include(p => p.ProductImages),
                    index,
                    pageSize
                );

            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }

        public async Task<double> GetDiscountedPrice(string productId, string inventoryId)
        {
            ProductInventory? productInventory = await _unitOfWork.GetRepository<ProductInventory>().Entities
                .Include(pi => pi.InventoryVariations)
                .FirstOrDefaultAsync(pi => pi.Id == inventoryId);

            //var productInventory = await _unitOfWork.GetRepository<ProductInventory>().Entities
            //    .Include(pi => pi.InventoryVariations)
            //    .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.InventoryVariations.Any(iv => iv.ProductVariationId == variationId));

            if (productInventory == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This product inventory is not found!");
            }

            Discount? discount = await _unitOfWork.GetRepository<Discount>().Entities
                    .Include(d => d.ProductDiscounts)
                    .FirstOrDefaultAsync(d => d.ProductDiscounts.Any(pd => pd.ProductId == productId));

            double percent;

            if (discount == null)
            {
                percent = 0;
            }
            else
            {
                percent = discount.DiscountPercent;
            } 

            double discountedPrice = productInventory.Price * (1 - percent / 100);

            return Math.Round(discountedPrice / 1000.0) * 1000 - 1000;
        }

        public async Task<IEnumerable<GetProductDto>> FilterProducts(string? brand, string? priceRange, string? capacity, string? sortBy)
        {
            var cacheKey = $"filtered_products:brand={brand}:priceRange={priceRange}:capacity={capacity}:sortBy={sortBy}";
            var cachedProducts = await _redisCacheService.GetCachedDataAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return JsonConvert.DeserializeObject<IEnumerable<GetProductDto>>(cachedProducts);
            }

            var query = _unitOfWork.GetRepository<Product>().Entities
                .Include(p => p.ProductInventories)
                    .ThenInclude(pi => pi.InventoryVariations)
                        .ThenInclude(iv => iv.ProductVariation)
                .Include(p => p.ProductImages)
                .AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(p => p.Brand == brand);
            }

            // Fetch the products into a list asynchronously
            var products = await query.ToListAsync();

            if (!string.IsNullOrEmpty(priceRange))
            {
                // Filter products based on price range
                var productsWithDiscountedPrices = new List<Product>();

                foreach (var product in products)
                {
                    foreach (var inventory in product.ProductInventories)
                    {
                        var discountedPrice = await GetDiscountedPrice(product.Id, inventory.Id);

                        if (priceRange.StartsWith(">"))
                        {
                            var minPrice = double.Parse(priceRange.Substring(1));
                            if (discountedPrice > minPrice)
                            {
                                productsWithDiscountedPrices.Add(product);
                                break;
                            }
                        }
                        else
                        {
                            var priceBounds = priceRange.Split('-').Select(double.Parse).ToArray();
                            if (discountedPrice >= priceBounds[0] && discountedPrice <= priceBounds[1])
                            {
                                productsWithDiscountedPrices.Add(product);
                                break;
                            }
                        }
                    }
                }

                // Filter the products to only those that meet the price criteria
                products = productsWithDiscountedPrices;
            }

            if (!string.IsNullOrEmpty(capacity))
            {
                // Filter by capacity
                products = products.Where(p => p.ProductInventories.Any(pi =>
                    pi.InventoryVariations.Any(iv =>
                        iv.ProductVariation.Type == "RAM" && iv.ProductVariation.Name == capacity))).ToList();
            }

            // Map Product entities to DTOs
            var productDtos = _mapper.Map<List<GetProductDto>>(products);

            // Set the OriginalPrice and DiscountedPrice
            foreach (var productDto in productDtos)
            {
                foreach (var inventory in productDto.ProductInventories)
                {
                    var productInventory = products
                        .First(p => p.Id == productDto.Id) // Find the corresponding Product entity
                        .ProductInventories
                        .First(pi => pi.Id == inventory.Id); // Find the corresponding ProductInventory entity

                    // Set the OriginalPrice and DiscountedPrice
                    var discountedPrice = await GetDiscountedPrice(productDto.Id, inventory.Id);
                    inventory.OriginalPrice = productInventory.Price; // Original price comes from the entity
                    inventory.DiscountPrice = discountedPrice; // Discounted price calculated separately
                }
            }

            // Sorting by DiscountedPrice in the DTO list
            if (!string.IsNullOrEmpty(sortBy))
            {
                productDtos = sortBy switch
                {
                    "high-low" => productDtos.OrderByDescending(p => p.ProductInventories.Max(pi => pi.DiscountPrice)).ToList(),
                    "low-high" => productDtos.OrderBy(p => p.ProductInventories.Min(pi => pi.DiscountPrice)).ToList(),
                    _ => productDtos
                };
            }

            await _redisCacheService.SetCachedDataAsync(cacheKey, JsonConvert.SerializeObject(productDtos), TimeSpan.FromMinutes(30));

            return productDtos;
        }



        public async Task<IEnumerable<GetProductDto>> SearchProduct(string productName)
        {
            var products = await _unitOfWork.GetRepository<Product>().Entities
                .Where(obj => obj.Model.Contains(productName))
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }

        public async Task PostProduct(PostProductDto model)
        {
            var product = new Product
            {
                Model = model.Model,
                Description = model.Description,
                Brand = model.Brand,
            };
            await _unitOfWork.GetRepository<Product>().InsertAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProduct(string productId)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(productId)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This product is not found!");

            var productImages = _unitOfWork.GetRepository<ProductImage>().Entities
                .Where(obj => obj.ProductId == productId);

            foreach (var image in productImages)
            {
                await _fileUploadHelper.DeleteFileAsync(image.ImageLink);
            }

            await _unitOfWork.GetRepository<Product>().DeleteAsync(product);
            await _unitOfWork.SaveAsync();
        }
    }
}