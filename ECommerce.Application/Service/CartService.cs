using AutoMapper;
using ECommerce.Application.DTOs.Cart;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Service
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;

        public CartService(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        private string GetCurrentUserId() => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

        public async Task<GetCartDto> GetCartAsync()
        {
            string userId = GetCurrentUserId() ?? throw new UnauthorizedAccessException("User must be logged in to access the cart.");
            
            Cart? cart = await _unitOfWork.GetRepository<Cart>().Entities
                .Include(c => c.CartItems)
                    .ThenInclude(c => c.ProductInventory)
                        .ThenInclude(c => c.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(c => c.ProductInventory)
                        .ThenInclude(c => c.InventoryVariations)
                            .ThenInclude(c => c.ProductVariation)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { Id = Guid.NewGuid().ToString("N"), UserId = userId };
                await _unitOfWork.GetRepository<Cart>().InsertAsync(cart);
                await _unitOfWork.SaveAsync();
            }

            return _mapper.Map<GetCartDto>(cart);
        }

        public async Task AddProductToCart(string productId, string inventoryId, int quantity, bool isModify = false)
        {
            // Get the current user ID
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("Người dùng cần đăng nhập để thêm sản phẩm vào giỏ hàng.");
            }

            // Retrieve or create a new cart for the user
            var cartRepository = _unitOfWork.GetRepository<Cart>();
            var cart = await cartRepository.Entities.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { Id = Guid.NewGuid().ToString("N"), UserId = userId, Total = 0 };
                await cartRepository.InsertAsync(cart);
            }

            // Retrieve the product inventory
            var inventoryRepository = _unitOfWork.GetRepository<ProductInventory>();
            var inventory = await inventoryRepository.Entities.FirstOrDefaultAsync(i => i.Id == inventoryId);
            if (inventory == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy kho hàng.");
            }

            // Retrieve the cart item if it exists
            var cartItemRepository = _unitOfWork.GetRepository<CartItem>();
            var cartItem = await cartItemRepository.Entities
                .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductInventoryId == inventoryId);

            // Calculate the discounted price
            double price = await _productService.GetDiscountedPrice(productId, inventoryId);

            // Retrieve the default product image
            var productImageRepository = _unitOfWork.GetRepository<ProductImage>();
            var image = await productImageRepository.Entities
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.IsDefaultImage == true);

            // Retrieve the product details
            var productRepository = _unitOfWork.GetRepository<Product>();
            var product = await productRepository.GetByIdAsync(productId);

            if (cartItem == null)
            {
                // Create a new cart item if it doesn't exist
                cartItem = new CartItem
                {
                    ProductInventoryId = inventoryId,
                    ItemPrice = price,
                    Quantity = quantity,
                    CartId = cart.Id,
                    Image = image?.ImageLink ?? string.Empty,
                    Model = product?.Model ?? string.Empty,
                    ProductInventory = inventory
                };

                cart.Total += price * quantity;
                await cartItemRepository.InsertAsync(cartItem);
            }
            else
            {
                // Update the quantity of the existing cart item
                if (isModify)
                {
                    cart.Total += price * (quantity - cartItem.Quantity);
                    cartItem.Quantity = quantity;
                }
                else
                {
                    cartItem.Quantity += quantity;
                    cart.Total += price * quantity;
                }
            }

            // Save changes
            await _unitOfWork.SaveAsync();
        }


        public async Task ChangeQuantityByOne(string cartId, string itemId, string option)
        {
            CartItem? cartItem = await _unitOfWork.GetRepository<CartItem>()
                .GetByIdAsync(itemId);

            Cart? cart = await _unitOfWork.GetRepository<Cart>().Entities.FirstOrDefaultAsync(c => c.Id == cartId);

            if (option == "increase")
            {
                cartItem.Quantity += 1;
                cart.Total += cartItem.ItemPrice;
            }
            else
            {
                cartItem.Quantity -= 1;
                cart.Total -= cartItem.ItemPrice;
            }

            await _unitOfWork.GetRepository<CartItem>().UpdateAsync(cartItem);
            await _unitOfWork.GetRepository<Cart>().UpdateAsync(cart);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveProductFromCart(string itemId)
        {
            string userId = GetCurrentUserId() ?? throw new UnauthorizedAccessException("User must be logged in to remove products from the cart.");
            Cart? cart = await _unitOfWork.GetRepository<Cart>().Entities.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart != null)
            {
                CartItem? productToRemove = await _unitOfWork.GetRepository<CartItem>().GetByIdAsync(itemId);
                if (productToRemove != null)
                {
                    cart.Total -= productToRemove.Quantity * productToRemove.ItemPrice;
                    await _unitOfWork.GetRepository<CartItem>().DeleteAsync(productToRemove.Id);
                    await _unitOfWork.SaveAsync();
                }
            }
        }
    }
}