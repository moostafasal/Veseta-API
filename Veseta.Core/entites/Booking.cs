using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.Core.entites
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public int DoctorAvailabilityId { get; set; }
        public virtual DoctorAvailability DoctorAvailability { get; set; }
        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }
        public int Status { get; set; }
        public DateTime BookingDate { get; set; }





    }
}
