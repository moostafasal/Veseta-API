using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.Enum;

namespace Veseta.Core.entites
{
    public class Appointment
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public virtual Days Day { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
