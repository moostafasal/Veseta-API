using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.Core.entites
{
    public class Doctor : ApplicationUser
    {
        public int SpecializationId { get; set; }
        public Specialization Specialzation { get; set; }
        public ICollection<Booking> requests { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
