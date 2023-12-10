
using Veseta.Core.entites;

namespace Veseta.Core.ISpecifications
{
    public class DiscountSpecification : BaseSpecification<Discount>
    {
        public DiscountSpecification()
        {

        }
        public DiscountSpecification(string couponCode) : base(d => d.DiscoundCode == couponCode)
        {

        }
    }
}
