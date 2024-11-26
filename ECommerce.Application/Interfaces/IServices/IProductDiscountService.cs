namespace ECommerce.Application.Interfaces.IServices
{
    public interface IProductDiscountService
    {
        Task PostProductDiscount(string productId, string discountId);
    }
}
