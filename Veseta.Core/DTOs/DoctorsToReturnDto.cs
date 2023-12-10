using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites.Enum;

namespace Veseta.CoreCore.DTOs
{
    public class DoctorsToReturnDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public int SpecializationId { get; set; }
    }
}
