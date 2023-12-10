using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites;
using Veseta.CoreCore.DTOs;


namespace Veseta.Core.IRepository
{
    public interface IAdminDashBoardRepository
    {
        Task<int> GetNumberOfDoctors();
        Task<int> GetNumberOfPatients();
        Task<Dictionary<BookingStatus,int>> GetNumberOfRequests();
        Task<ICollection<TopFiveSpecializationsDTO>> GetTopFiveSpecializations();
        Task<ICollection<TopTenDoctorsDTO>> GetTopTenDoctors();
       

    }
}
