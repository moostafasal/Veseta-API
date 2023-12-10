using AutoMapper;

using Veseta.Core.entites;
using Veseta.Core.Helper;
using Veseta.Core.IRepository;
using Veseta.Core.IServices;
using Veseta.Core.ISpecifications;
using Veseta.Core.UnitOfWork;
using Veseta.CoreCore.DTOs;



namespace Veseta.CoreService
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public DoctorService(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IDoctorRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _doctorRepository = repository;
            _mapper = mapper;
        }

        public async Task<int> ConfirmCheckUp(int bookingId)
        {
           Booking booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
            booking.BookingState = BookingStatus.Completed;


            _unitOfWork.Repository<Booking>().Update(booking);

            return await _unitOfWork.Complete();
        }

        public async Task<IList<AppointmentToReturnDto>> CreateAppointment(AppointmentDto model , string doctorEmail)
        {
            var doctor = await _doctorRepository.FindDoctorByEmail(doctorEmail);
            var appointmentSlots = new List<Appointment>();

            foreach (var slot in model.TimeSlots)
            {
                var appointmentSlot = new Appointment
                {
                    Day = slot.Day,
                    Price = model.Price,
                    DoctorId = doctor.Id,
                    TimeSlots = slot.Times.Select(time => new TimeSlot { Time = time, Booked = false }).ToList()
                };
                appointmentSlots.Add(appointmentSlot);
            }

            doctor.Appointments = appointmentSlots;

            await _unitOfWork.Complete();
            var spec = new AppointmentSpecification(doctor.Id);
            var updatedAppointments = await _unitOfWork.Repository<Appointment>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IList<Appointment>,IList<AppointmentToReturnDto>>(updatedAppointments);
        }

        public async Task<int> DeleteAppointmentAsync(IList<int> timeSlotIds)
        {
            IList<TimeSlot> timeSlots = await _unitOfWork.Repository<TimeSlot>().GetPatchById(timeSlotIds);
            if (timeSlots.Any(t => t.Booked))
                return 0;
            _unitOfWork.Repository<TimeSlot>().DeletePatch(timeSlots);
            int result = await _unitOfWork.Complete();
            return result;

        }

        public async Task<IList<BookingsForDoctorToReturnDTO>> GetAllBookingsForDoctor(string doctorEmail , Paging paging)
        {
            Doctor doctor = await _doctorRepository.FindDoctorByEmail(doctorEmail);
            IList<BookingsForDoctorToReturnDTO> bookings = await _doctorRepository.GetBookingsForDoctor(doctor.Id ,paging);

            return bookings;
        }

        public async Task<IList<AppointmentToReturnDto>> GetDoctorWithAppointement(Paging paging)
        {
            return await _doctorRepository.GetDoctorsWithAppointments(paging);
        }

        public async Task<int> UpdateAppointmentAsync(AppointmentDto model , string doctorEmail)
        {
            Doctor doctor = await _doctorRepository.GetDoctorWithAppointment(doctorEmail);
            foreach (var day in doctor.Appointments)
            {
                var updatedDay = model.TimeSlots.Where(D => D.Day == day.Day).FirstOrDefault();

                if (updatedDay != null)
                { 
                    if (day.TimeSlots.Any(t => t.Booked))
                    {
                        return 0;
                    }
                    var time =   updatedDay.Times.Select(t => new TimeSlot
                    {
                         Time= t
                    });

                    day.TimeSlots = time.ToList();
                    day.Price = model.Price;
                }
            }

            
            return await _unitOfWork.Complete();

        }


       
    }
}
