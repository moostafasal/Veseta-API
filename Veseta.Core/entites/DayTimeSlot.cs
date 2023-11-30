using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.Core.entites
{
    public class DayTimeSlot
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public int TimeSlotId { get; set; }

        // Navigation properties
        public virtual days Day { get; set; }// enum
        public virtual TimeSlot TimeSlot { get; set; }
    }
}
