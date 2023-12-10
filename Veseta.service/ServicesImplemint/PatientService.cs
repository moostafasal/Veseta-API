using Veseta.Core.entites;
using Veseta.Core.entites.Enum;
using Veseta.Core.IRepository;
using Veseta.Core.IServices;
using Veseta.Core.ISpecifications;
using Veseta.Core.UnitOfWork;
using Veseta.CoreCore.DTOs;

namespace Veseta.CoreService
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _patientRepo;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepo)
        {
            _unitOfWork = unitOfWork;
            _patientRepo = patientRepo;
        }

        public async Task<int> Booking(BookingDTO bookingDTO, string patientEmail)
        {
            Discount coupon = await GetDiscountCoupon(bookingDTO.DiscountCouponName);

            Booking booking = await CreateBooking(bookingDTO, patientEmail, coupon);

            await UpdateTimeSlotAndSaveChanges(bookingDTO.TimeId, true);

            return await _unitOfWork.Complete();
        }

        public async Task<int> CancelBooking(int bookingId)
        {
            Booking booking = await GetBookingWithSpec(bookingId);

            booking.BookingState = BookingStatus.Canceled;

            await UpdateTimeSlotAndSaveChanges(booking.TimeSlotId, false);

            return await _unitOfWork.Complete();
        }

        public async Task<IList<BookingsForPatientToReturnDTO>> GetAllBookings(string patientEmail)
        {
            Patient patient = await _patientRepo.FindPatientByEmail(patientEmail);
            IList<BookingsForPatientToReturnDTO> bookings = await _patientRepo.GetBookingsForPatient(patient.Id);

            return bookings;
        }

        private async Task<Discount> GetDiscountCoupon(string discountCouponName)
        {
            if (discountCouponName == null)
                return null;

            DiscountSpecification spec = new DiscountSpecification(discountCouponName);
            return await _unitOfWork.Repository<Discount>().GetByIdWithSpecAsync(spec);
        }

        private async Task<Booking> CreateBooking(BookingDTO bookingDTO, string patientEmail, Discount coupon)
        {
            Patient patient = await _patientRepo.FindPatientByEmail(patientEmail);
            TimeSlot updatedSlot = await GetTimeSlotByIdWithSpec(bookingDTO.TimeId);

            Booking booking = new Booking
            {
                Price = updatedSlot.Appointment.Price,
                FinalPrice = coupon != null && coupon.IsActive
                    ? CalculateFinalPrice(coupon, updatedSlot.Appointment.Price)
                    : updatedSlot.Appointment.Price,
                DoctorId = updatedSlot.Appointment.DoctorId,
                PatientId = patient.Id,
                DiscountCodeCouponId = coupon?.Id,
                TimeSlotId = bookingDTO.TimeId
            };

            await _unitOfWork.Repository<Booking>().Add(booking);

            return booking;
        }

        private static decimal CalculateFinalPrice(Discount coupon, decimal price)
        {
            return coupon.DiscountType == DiscountType.Percentage
                ? price - (price * (coupon.Value / 100))
                : price - coupon.Value;
        }

        private async Task UpdateTimeSlotAndSaveChanges(int timeSlotId, bool isBooked)
        {
            TimeSlot updatedSlot = await GetTimeSlotByIdWithSpec(timeSlotId);
            updatedSlot.Booked = isBooked;

            _unitOfWork.Repository<TimeSlot>().Update(updatedSlot);
        }

        private Task<TimeSlot> GetTimeSlotByIdWithSpec(int timeSlotId)
        {
            TimeSlotSpecification timeSpec = new TimeSlotSpecification(timeSlotId);
            return _unitOfWork.Repository<TimeSlot>().GetByIdWithSpecAsync(timeSpec);
        }

        private Task<Booking> GetBookingWithSpec(int bookingId)
        {
            var spec = new BookingSpec(bookingId);
            return _unitOfWork.Repository<Booking>().GetByIdWithSpecAsync(spec);
        }
    }
}
