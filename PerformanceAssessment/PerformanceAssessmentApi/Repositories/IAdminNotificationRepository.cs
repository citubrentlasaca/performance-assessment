using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IAdminNotificationRepository
    {
        Task<int> CreateAdminNotification(AdminNotification adminNotification);

        Task<IEnumerable<AdminNotificationDto>> GetAllAdminNotifications();

        Task<AdminNotificationDto> GetAdminNotificationById(int id);
    }
}
