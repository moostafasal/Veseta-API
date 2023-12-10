using Veseta.Core.entites.Enum;


namespace Veseta.CoreAPI.DTOs
{
    public class DiscountCodeCouponDTO
    {
        public string DiscoundCode { get; set; }
        public int NumberOfRequests { get; set; }
        public decimal Value { get; set; }
        public DiscountType DiscountType { get; set; }
        public bool IsActive { get; set; }
    }
}
