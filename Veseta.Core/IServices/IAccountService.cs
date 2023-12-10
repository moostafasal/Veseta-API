
using System.Threading.Tasks;
using Veseta.Core.entites;
using Veseta.CoreAPI.DTOs;
using Veseta.CoreCore.DTOs;

namespace Veseta.Core.IServices
{
    public interface IAccountService
    {
        Task<ApplicationUser?> AddUser(UserDTO user, string roleName ,int? SpecializationId);
        Task<bool> UpdateUser(UserDTO userUpdated, int specialization);
        Task<IList<ApplicationUser>> GetUsersInRole(string roleName);
        Task<ApplicationUser?> GetUserInRoleById(string roleName , string id);
        Task<ApplicationUser?> Login(LoginDto model);
        Task<int> GetCount();
        Task<bool> DeleteDoctor(string id);
    }
}
