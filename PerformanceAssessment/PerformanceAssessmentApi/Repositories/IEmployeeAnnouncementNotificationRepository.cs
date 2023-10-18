using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployeeAnnouncementNotificationRepository
    {
        Task<int> CreateEmployeeAnnouncementNotification(EmployeeAnnouncementNotification employeeNotification);

        Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetAllEmployeeAnnouncementNotifications();

        Task<EmployeeAnnouncementNotificationDto> GetEmployeeAnnouncementNotificationById(int id);

        Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetEmployeeAnnouncementNotificationByEmployeeId(int id);
    }
}
