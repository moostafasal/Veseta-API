namespace Veseta.Core.entites
{
    public class TimeSlot  : BaseEntity
    {
        public TimeSlot()
        {

        }
        public TimeSlot(string time, bool Booked)
        {
            Time = time;
           Booked = Booked;
        }

        public string Time { get; set; }
        public bool Booked { get; set; } = false;

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}