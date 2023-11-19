using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IAssignSchedulerRepository
    {
        Task<IEnumerable<int>> CreateAssignSchedulers(IEnumerable<int> employeeIds, AssignScheduler assignScheduler);
        Task<IEnumerable<AssignSchedulerDto>> GetAllAssignSchedulers();
        Task<AssignSchedulerDto> GetAssignSchedulerById(int id);
        Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByAssessmentId(int assessmentId);
        Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByEmployeeId(int employeeId);
        Task<int> UpdateAssignScheduler(AssignScheduler assignScheduler);
        Task<int> DeleteAssignScheduler(int id);
        Task<AssignSchedulerDto> GetAssignSchedulerByEmployeeIdAndAssessmentId(int employeeId, int assessmentId);
    }
}