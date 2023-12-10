namespace Veseta.CoreCore.DTOs
{
    public class TimeSlotDto
    {
        public DayOfWeek Day { get; set; }
        public ICollection<string> Times { get; set; }
    }
}