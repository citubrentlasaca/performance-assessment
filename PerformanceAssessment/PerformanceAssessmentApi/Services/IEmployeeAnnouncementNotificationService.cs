using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployeeAnnouncementNotificationService
    {
        Task<EmployeeAnnouncementNotification> CreateEmployeeAnnouncementNotification(EmployeeAnnouncementNotificationCreationDto employeeNotification);

        Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetAllEmployeeAnnouncementNotifications();

        Task<EmployeeAnnouncementNotificationDto> GetEmployeeAnnouncementNotificationById(int id);

        Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetEmployeeAnnouncementNotificationByEmployeeId(int id);
    }
}
