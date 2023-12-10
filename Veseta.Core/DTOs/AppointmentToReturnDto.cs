using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.DTOs;
using Veseta.Core.entites.Enum;

namespace Veseta.CoreCore.DTOs
{
    public class AppointmentToReturnDto
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public string ImageUrl { get; set; }
        public Gender Gender { get; set; }
        public string SpecializationName { get; set; }
        public int SpecializationId { get; set; }
        public ICollection<AppointmentVM> AppointmentDTO { get; set; } 

    }
}
