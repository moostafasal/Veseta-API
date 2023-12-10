using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.CoreCore.DTOs;


namespace Veseta.Core.IServices
{
    public interface IPatientService
    {
        Task<int> Booking(BookingDTO bookingDTO , string patientEmail);
        Task<IList<BookingsForPatientToReturnDTO>> GetAllBookings(string patientEmail);
        Task<int> CancelBooking(int bookingId);
    }
}
