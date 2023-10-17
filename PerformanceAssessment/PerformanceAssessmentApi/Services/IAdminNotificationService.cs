using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IAdminNotificationService
    {
        Task<AdminNotification> CreateAdminNotification(AdminNotificationCreationDto adminNotification);

        Task<IEnumerable<AdminNotificationDto>> GetAllAdminNotifications();

        Task<AdminNotificationDto> GetAdminNotificationById(int id);
    }
}
