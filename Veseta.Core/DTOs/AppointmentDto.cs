using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.CoreCore.DTOs
{
    public class AppointmentDto
    {
        public decimal Price { get; set; }
        public ICollection<TimeSlotDto> TimeSlots { get; set; }
    }
}
