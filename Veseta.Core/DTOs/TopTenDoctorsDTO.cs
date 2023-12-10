using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.CoreCore.DTOs
{
    public class TopTenDoctorsDTO
    {
        public string ImageUrl { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public int NumberOfRequests { get; set; }
    }
}
