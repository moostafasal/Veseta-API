using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.Core.entites
{
    public class Patient : ApplicationUser 
    {
        public ICollection<Booking> Requests { get; set; }
    }
}
