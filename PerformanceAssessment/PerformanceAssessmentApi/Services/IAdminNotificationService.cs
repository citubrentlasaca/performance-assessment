using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IAdminNotificationService
    {
        Task<AdminNotification> CreateAdminNotification(AdminNotificationCreationDto adminNotification);

        Task<IEnumerable<AdminNotificationDto>> GetAllAdminNotifications();

        Task<AdminNotificationDto> GetAdminNotificationById(int id);

        Task<IEnumerable<AdminNotificationDto>> GetAdminNotificationByEmployeeId(int employeeId);

        Task ExecuteAdminNotificationAsync(int schedulerId, AdminNotificationCreationDto adminNotification);
    }
}
