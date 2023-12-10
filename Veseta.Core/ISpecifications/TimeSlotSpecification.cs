

using Veseta.Core.entites;

namespace Veseta.Core.ISpecifications
{
    public class TimeSlotSpecification : BaseSpecification<TimeSlot>
    {
        public TimeSlotSpecification(int id) : base(t => t.Id == id)
        {
            Includes.Add(ts => ts.Appointment);
        }
    }
}
