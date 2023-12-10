

using Veseta.Core.entites;
using Veseta.Core.Helper;
using Veseta.CoreCore.DTOs;

namespace Veseta.Core.IRepository
{
    
    public interface IDoctorRepository
    {
        Task<Doctor> FindDoctorById(string id);
        Task<Doctor> FindDoctorByEmail(string email);
        Task<IList<AppointmentToReturnDto>> GetDoctorsWithAppointments(Paging paging);
        Task<Doctor> GetDoctorWithAppointment(string doctorEmail);
        Task<Doctor> GetDoctorWithBookings(string doctorId);

        Task<IList<BookingsForDoctorToReturnDTO>> GetBookingsForDoctor(string doctorId, Paging paging);
        void SaveChanges();
    }
}
