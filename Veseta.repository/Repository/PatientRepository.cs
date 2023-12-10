using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;
using Veseta.Core.IRepository;
using Veseta.CoreCore.DTOs;
using Veseta.repository.Data;

namespace Veseta.CoreRepository.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> FindPatientByEmail(string email)
        {
            return await _context.patients.Where(p => p.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IList<BookingsForPatientToReturnDTO>> GetBookingsForPatient(string patientId)
        {
            IList<BookingsForPatientToReturnDTO> bookings = await _context.Bookings.Where(b => b.PatientId == patientId)
                                                 .Select(p => new BookingsForPatientToReturnDTO
                                                 {
                                                     Day = p.TimeSlot.Appointment.Day,
                                                     DiscountCodeName = p.DiscountCodeCoupon == null ? "" : p.DiscountCodeCoupon.DiscoundCode,
                                                     FinalPrice = p.FinalPrice,
                                                     DoctorName = p.Doctor.FirstName,
                                                     ImageUrl = p.Doctor.ImageUrl,
                                                     Price = p.Price,
                                                     Status = p.BookingState,
                                                     SpecializationName = p.Doctor.Specialzation.SpecializationName,
                                                     Time = p.TimeSlot.Time
                                                 }).ToListAsync();
            return bookings;
        }
    }
}
