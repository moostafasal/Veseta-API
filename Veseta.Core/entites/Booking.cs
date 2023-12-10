
using System.ComponentModel.DataAnnotations.Schema;

namespace Veseta.Core.entites
{
    public class Booking : BaseEntity
    {

        public BookingStatus BookingState { get; set; } = BookingStatus.Pending; 

        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }

        [ForeignKey("Discount")]
        public int? DiscountCodeCouponId { get; set; }
        public Discount? DiscountCodeCoupon { get; set; }

        // Nav prop for patient 
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        [InverseProperty("Requests")]

        public Patient Patient { get; set; }

        // Nav Prop for Doctor
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("TimeSlot")]

        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }



    }
}
