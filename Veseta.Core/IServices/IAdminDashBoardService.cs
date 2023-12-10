using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.CoreCore.DTOs;


namespace Veseta.Core.IServices
{
    public interface IAdminDashBoardService
    {
        Task<NumberOfBookingRequestsDTO> GetNumberOfRequests();
        Task<ICollection<TopTenDoctorsDTO>> GetTopTenDoctors();
        Task<ICollection<TopFiveSpecializationsDTO>> GetTopFiveSpecialization();

    }
}
