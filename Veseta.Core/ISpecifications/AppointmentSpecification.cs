using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;

namespace Veseta.Core.ISpecifications
{
    public class AppointmentSpecification : BaseSpecification<Appointment> 
    {
        public AppointmentSpecification(string doctorId) : base(O => O.DoctorId == doctorId)
        {
            Includes.Add(o => o.TimeSlots);
        }
        public AppointmentSpecification(int appointmentId) : base (Object => Object.Id == appointmentId)
        {
            Includes.Add(o => o.TimeSlots);
        }
    }
}
