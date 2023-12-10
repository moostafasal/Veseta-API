using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.Helper;
using Veseta.CoreCore.DTOs;


namespace Veseta.Core.IServices
{
    public interface IDoctorService
    {
        Task<IList<AppointmentToReturnDto>> CreateAppointment(AppointmentDto model, string doctotId);
        Task<int> UpdateAppointmentAsync(AppointmentDto model, string doctorEmail);
        Task<int> DeleteAppointmentAsync(IList<int> timeSlotsIds);
        Task<IList<AppointmentToReturnDto>> GetDoctorWithAppointement(Paging paging);
        Task<IList<BookingsForDoctorToReturnDTO>> GetAllBookingsForDoctor(string doctorEmail ,Paging paging);
        Task<int> ConfirmCheckUp(int bookingId);

    }
}
