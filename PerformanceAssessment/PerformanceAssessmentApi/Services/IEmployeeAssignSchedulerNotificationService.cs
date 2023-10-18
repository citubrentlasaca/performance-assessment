using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployeeAssignSchedulerNotificationService
    {
        Task<EmployeeAssignSchedulerNotification> CreateEmployeeAssignSchedulerNotification(EmployeeAssignSchedulerNotificationCreationDto employeeNotification);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotifications();

        Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeAssignSchedulerNotificationById(int id);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(int employeeId);
    }
}
