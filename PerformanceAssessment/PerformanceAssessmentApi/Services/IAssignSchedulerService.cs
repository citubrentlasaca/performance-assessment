using PerformanceAssessmentApi.Dtos;

namespace PerformanceAssessmentApi.Services
{
    public interface IAssignSchedulerService
    {
        Task<IEnumerable<int>> CreateAssignSchedulers(IEnumerable<int> employeeIds, AssignSchedulerCreationDto assignScheduler);
        Task<IEnumerable<AssignSchedulerDto>> GetAllAssignSchedulers();
        Task<AssignSchedulerDto> GetAssignSchedulerById(int id);
        Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByAssessmentId(int assessmentId);
        Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByEmployeeId(int employeeId);
        Task<int> UpdateAssignScheduler(int id, AssignSchedulerUpdationDto schedule);
        Task<int> DeleteAssignScheduler(int id);
    }
}
