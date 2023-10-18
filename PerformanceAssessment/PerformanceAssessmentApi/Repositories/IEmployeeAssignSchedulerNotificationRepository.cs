using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployeeAssignSchedulerNotificationRepository
    {
        Task<int> CreateEmployeeAssignSchedulerNotification(EmployeeAssignSchedulerNotification employeeNotification);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotifications();

        Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeAssignSchedulerNotificationById(int id);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(int employeeId);
    }
}
