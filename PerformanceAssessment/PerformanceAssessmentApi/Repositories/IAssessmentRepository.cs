using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IAssessmentRepository
    {
        Task<int> CreateAssessment(Assessment assessment);

        Task<IEnumerable<AssessmentDto>> GetAllAssessments();

        Task<AssessmentDto> GetAssessmentById(int id);

        Task<int> UpdateAssessment(Assessment assessment);

        Task<int> DeleteAssessment(int id);

        Task<AssessmentItemDto?> GetAssessmentItemsById(int id);

        Task<IEnumerable<AssessmentDto>> GetAssessmentsByEmployeeId(int employeeId);
    }
}