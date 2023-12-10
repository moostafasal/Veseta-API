using Veseta.Core.entites;
using Veseta.Core.IRepository;
using Veseta.Core.IServices;
using Veseta.CoreCore.DTOs;

namespace Veseta.CoreService
{
    public class AdminDashBoardService : IAdminDashBoardService
    {
        private readonly IAdminDashBoardRepository _adminDashRepo;

        public AdminDashBoardService(IAdminDashBoardRepository adminDashRepo)
        {
            _adminDashRepo = adminDashRepo;
        }

        public async Task<NumberOfBookingRequestsDTO> GetNumberOfRequests()
        {
            var counts = await _adminDashRepo.GetNumberOfRequests();

            int pending = GetCount(counts, BookingStatus.Pending);
            int completed = GetCount(counts, BookingStatus.Completed);
            int canceled = GetCount(counts, BookingStatus.Canceled);
            int totalRequests = pending + completed + canceled;

            return new NumberOfBookingRequestsDTO
            {
                TotalNumberOfRequests = totalRequests,
                NumberOfPending = pending,
                NumberOfCompleted = completed,
                NumberOfCancelled = canceled
            };
        }

        private static int GetCount(Dictionary<BookingStatus, int> counts, BookingStatus status)
        {
            return counts.ContainsKey(status) ? counts[status] : 0;
        }

        public Task<ICollection<TopFiveSpecializationsDTO>> GetTopFiveSpecialization()
        {
            return _adminDashRepo.GetTopFiveSpecializations();
        }

        public Task<ICollection<TopTenDoctorsDTO>> GetTopTenDoctors()
        {
            return _adminDashRepo.GetTopTenDoctors();
        }
    }
}
