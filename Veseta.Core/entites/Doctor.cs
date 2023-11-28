using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.Core.entites
{
    public class Doctor:AppUsers
    {
        //FK form AppUsers
        [Key]
        public int Id { get; set; }

        [Required]
        public string pictureUrl { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }


    }
}
