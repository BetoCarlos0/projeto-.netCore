using VCommerce.CartApi.DTOs;

namespace VCommerce.DiscountApi.Repositories
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string couponCode);
    }
}
