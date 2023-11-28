using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.Enum;

namespace Veseta.Core.entites
{

        public class DoctorAvailability
        {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DayId { get; set; }

        // Navigation properties
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<DayTimeSlot> DayTimeSlots { get; set; }


        // ... other properties related to doctor availability

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<AppointmentRequest> AppointmentRequests { get; set; }
    }
    
}
