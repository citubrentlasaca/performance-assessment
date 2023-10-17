using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployeeNotificationRepository
    {
        Task<int> CreateEmployeeNotification(EmployeeNotification employeeNotification);

        Task<IEnumerable<EmployeeNotificationDto>> GetAllEmployeeNotifications();

        Task<EmployeeNotificationDto> GetEmployeeNotificationById(int id);
    }
}
