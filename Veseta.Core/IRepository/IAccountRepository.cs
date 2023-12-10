
using Veseta.Core.entites;

namespace Veseta.Core.IRepository
{
    public interface IAccountRepository 
    {
        Task<bool> CreateUser(ApplicationUser user , string password);
        Task AddRole(ApplicationUser user , string roleName);
        Task<ApplicationUser?> GetUserById(string id);
        Task<bool> UpdateUser(Doctor user);
        Task<Doctor> GetDoctorByEmail(string Email);
        Task<ApplicationUser> GetUserByEmail(string Email);
        Task<IList<ApplicationUser>> GetUsersInRole(string roleName);
        Task<bool> IsUserInRole(string roleName , ApplicationUser user);
        void DeleteDoctor(Doctor doctor);
        Task SaveChanges();
    }
}
