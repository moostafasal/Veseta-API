
using Veseta.Core.entites;
using Veseta.CoreAPI.DTOs;
using Veseta.CoreCore.DTOs;

namespace Veseta.Core.IServices
{
    public interface IDiscountCouponService
    {
        Task<int> AddNewCoupon(DiscountCodeCouponDTO model);
        Task<IReadOnlyList<Discount>> GetAllCoupons();
        Task<int> DeleteCoupon(int id);
        Task<int> DeActivateCoupon(int id);
        Task<int> UpdateCoupon(DiscountToUpdateDto model);
    }
}
