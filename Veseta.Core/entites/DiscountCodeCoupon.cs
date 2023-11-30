using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.Enum;

namespace Veseta.Core.entites
{
    public class DiscountCodeCoupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Requests { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
