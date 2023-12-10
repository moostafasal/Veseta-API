using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Veseta.Core.entites;
using Veseta.Core.IRepository;
using Veseta.CoreCore.DTOs;
using Veseta.repository.Data;

namespace Veseta.CoreRepository.Repository
{
    public class AdminDashBoardRepository : IAdminDashBoardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminDashBoardRepository(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public Task<Doctor> AddDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Doctor>> GetAllDoctors()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetDoctorById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumberOfDoctors()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumberOfPatients()
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<BookingStatus,int>> GetNumberOfRequests()
        {
            return await _context.Bookings
                .GroupBy(b => b.BookingState)
                .Select(group => new
                {
                    Status = group.Key,
                    Count = group.Count()
                })
                .ToDictionaryAsync(item => item.Status, item => item.Count);
        }

        public async Task<ICollection<TopFiveSpecializationsDTO>> GetTopFiveSpecializations()
        {
            var topFiveSpecializations = await _context.Specializations
                .Join(
                    _context.Doctors,
                    specialization => specialization.Id,
                    doctor => doctor.SpecializationId,
                    (specialization, doctor) => new { Specialization = specialization, Doctor = doctor }
                )
                .Join(
                    _context.Bookings,
                    joined => joined.Doctor.Id,
                    booking => booking.DoctorId,
                    (joined, booking) => new { Specialization = joined.Specialization, Booking = booking }
                )
                .GroupBy(joined => new { joined.Specialization.Id, joined.Specialization.SpecializationName })
                .OrderByDescending(group => group.Count())
                .Take(5)
                .Select(group => new TopFiveSpecializationsDTO
                {
                    SpecializationName = group.Key.SpecializationName,
                    NumberOfRequests = group.Count()
                })
                .ToListAsync();

            return topFiveSpecializations;
        }

        public async Task<ICollection<TopTenDoctorsDTO>> GetTopTenDoctors()
        {
            var topTenDoctors = await _context.Doctors
                .Join(
                    _context.Bookings,
                    doctor => doctor.Id,
                    booking => booking.DoctorId,
                    (doctor, booking) => new { Doctor = doctor, Booking = booking }
                )
                .GroupBy(joined => new { joined.Doctor.Id, joined.Doctor.FirstName , joined.Doctor.ImageUrl , joined.Doctor.Specialzation.SpecializationName})
                .OrderByDescending(group => group.Count())
                .Take(10)
                .Select(group => new TopTenDoctorsDTO
                {
                    
                    DoctorName = group.Key.FirstName,
                    NumberOfRequests = group.Count(),
                    ImageUrl = group.Key.ImageUrl,
                    Specialization = group.Key.SpecializationName
                })
                .ToListAsync();

            return topTenDoctors;
        }

        public Task UpdateDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
