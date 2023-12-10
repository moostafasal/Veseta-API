
using Veseta.Core.entites;
using Veseta.CoreCore.DTOs;


namespace Veseta.Core.IRepository
{
    public interface IPatientRepository
    {
        Task<Patient> FindPatientByEmail(string email);
        Task<IList<BookingsForPatientToReturnDTO>> GetBookingsForPatient(string patientId);
    }
}
