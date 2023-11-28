using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Veseta.Core.Enum;

namespace Veseta.Core.entites
{
    public class AppUsers: IdentityUser
    {
        public string usertype { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public virtual ICollection<AppointmentRequest> AppointmentRequest { get; set; }





    }
}
