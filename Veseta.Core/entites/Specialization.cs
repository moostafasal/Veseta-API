using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.Core.entites
{
    public class Specialization
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int SpecializationLookupId { get; set; }

        // Navigation properties
        public virtual Doctor Doctor { get; set; }
        public virtual SpecializationLookup SpecializationLookup { get; set; }
    }
}
