using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployeeAssignSchedulerNotificationService
    {
        Task<EmployeeAssignSchedulerNotification> CreateEmployeeNotification(EmployeeAssignSchedulerNotificationCreationDto employeeNotification);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotifications();

        Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeNotificationById(int id);

        Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotificationsByEmployeeId(int employeeId);
    }
}
