using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Veseta.CoreCore.DTOs
{
    public class BookingsForDoctorToReturnDTO
    {
        public string ImageUrl { get; set; }
        public string PatientName { get; set; }
        public DayOfWeek Day { get; set; }
        public string Time { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

    }
}
