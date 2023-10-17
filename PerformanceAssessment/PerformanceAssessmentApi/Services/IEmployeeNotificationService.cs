using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployeeNotificationService
    {
        Task<EmployeeNotification> CreateEmployeeNotification(EmployeeNotificationCreationDto employeeNotification);

        Task<IEnumerable<EmployeeNotificationDto>> GetAllEmployeeNotifications();

        Task<EmployeeNotificationDto> GetEmployeeNotificationById(int id);
    }
}
