using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.DTOs;
using Veseta.Core.entites;
using Veseta.Core.Helper;
using Veseta.Core.IRepository;
using Veseta.CoreCore.DTOs;
using Veseta.repository.Data;


namespace Veseta.CoreRepository.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> FindDoctorByEmail(string email)
        {
            return await _context.Doctors.Where(d => d.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Doctor> FindDoctorById(string id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<IList<BookingsForDoctorToReturnDTO>> GetBookingsForDoctor(string doctorId ,Paging paging)
        {
            IList<BookingsForDoctorToReturnDTO> bookings = await _context.Bookings.Where(b => b.DoctorId == doctorId)
                                                 .Select(p => new BookingsForDoctorToReturnDTO
                                                 {
                                                     Day = p.TimeSlot.Appointment.Day,
                                                     PatientName = p.Patient.FirstName,
                                                     ImageUrl = p.Patient.ImageUrl,
                                                     Time = p.TimeSlot.Time,
                                                     Email= p.Patient.Email,
                                                     PhoneNumber = p.Patient.PhoneNumber,
                                                     Age = (DateTime.Today.Year - p.Patient.DateOfBirth.Year)
                                                 }).Skip((paging.PageNumber-1)*paging.PageSize).Take(paging.PageSize).ToListAsync();
            return bookings;
        }

        public async Task<IList<AppointmentToReturnDto>> GetDoctorsWithAppointments(Paging paging)
        {
            IList<AppointmentToReturnDto> appointmentToReturnDtos =
                           await _context.Doctors.Include(d => d.Appointments)
                                                   .ThenInclude(d => d.TimeSlots)
                                                   .Select(a => new AppointmentToReturnDto
                                                   {
                                                       DoctorEmail = a.Email,
                                                       Gender = a.Gender,
                                                       DoctorName = a.FirstName,
                                                       DoctorId = a.Id,
                                                       ImageUrl = a.ImageUrl,
                                                       SpecializationId = a.SpecializationId,
                                                       SpecializationName = a.Specialzation.SpecializationName,
                                                       AppointmentDTO = a.Appointments.Select(ap => new AppointmentVM
                                                       {
                                                           Day = ap.Day,
                                                           Price = ap.Price,
                                                           Times = ap.TimeSlots.Where(ap => ap.Booked == false).Select(ts => new TimeSlotVM
                                                           {
                                                               Time = ts.Time,
                                                               IsBooked = ts.Booked
                                                           }).ToList(),
                                                       }).ToList()

                                                   }).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();
            return appointmentToReturnDtos;
        }

        public async Task<Doctor> GetDoctorWithAppointment(string doctorEmail)
        {
            return await _context.Doctors.Where(d => d.Email == doctorEmail).Include(d => d.Appointments).ThenInclude(d => d.TimeSlots).FirstOrDefaultAsync();
        }
        public async Task<Doctor> GetDoctorWithBookings(string doctorId )
        {
            return await _context.Doctors.Where(d => d.Id == doctorId).Include(d => d.requests).FirstOrDefaultAsync();
        }

        public void SaveChanges()
        {
             _context.SaveChanges();
        }
    }
}
