using Microsoft.AspNetCore.Identity;
using Veseta.Core.entites;
using Veseta.Core.IRepository;
using Veseta.repository.Data;

namespace Veseta.CoreRepository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AccountRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task AddRole(ApplicationUser user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> CreateUser(ApplicationUser user , string password)
        {
            var result = await _userManager.CreateAsync(user , password);
            if(result.Succeeded) { return true; }
            else { return false; }
        }

        public void DeleteDoctor(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
        }

        public async Task<Doctor> GetDoctorByEmail(string email)
        {
            return (Doctor)await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IList<ApplicationUser>> GetUsersInRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users;
        }

        public async Task<bool> IsUserInRole(string roleName, ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user,roleName);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(Doctor user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;
            return true;
        }
    }
}
