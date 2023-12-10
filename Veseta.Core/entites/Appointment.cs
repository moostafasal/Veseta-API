using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Veseta.Core.entites
{
    public class Appointment : BaseEntity
    {
        public Appointment()
        {

        }
        public Appointment(decimal price, DayOfWeek day, ICollection<TimeSlot> timeSlots, string doctorId)
        {
            Price = price;
            Day = day;
            TimeSlots = timeSlots;
            DoctorId = doctorId;
        }

        public decimal Price { get; set; }
        public DayOfWeek Day { get; set; }
        public ICollection<TimeSlot> TimeSlots { get; set; }

        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }



    }
}
