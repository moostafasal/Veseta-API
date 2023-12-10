using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;

namespace Veseta.Core.ISpecifications
{
    public class BookingSpec : BaseSpecification<Booking>
    {
        public BookingSpec(int bookingId) : base(b => b.Id == bookingId)
        {
            Includes.Add(o => o.TimeSlot);
        }
    }
}
