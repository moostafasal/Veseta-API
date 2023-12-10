using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.Core.DTOs
{
    public class AppointmentVM
    {
        public decimal Price { get; set; }
        public DayOfWeek Day { get; set; }
        public ICollection<TimeSlotVM> Times { get; set; }
    }
}
