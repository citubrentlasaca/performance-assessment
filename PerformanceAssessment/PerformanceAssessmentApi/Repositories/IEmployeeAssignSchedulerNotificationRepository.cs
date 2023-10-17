using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployeeAssignSchedulerNotificationRepository
    {
        Task<int> CreateEmployeeNotification(EmployeeAssignSchedulerNotification employeeNotification);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotifications();

        Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeNotificationById(int id);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotificationsByEmployeeId(int employeeId);
    }
}
