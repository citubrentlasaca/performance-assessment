using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IAssessmentService
    {
        Task<Assessment> CreateAssessment(AssessmentCreationDto assessment);

        Task<IEnumerable<AssessmentDto>> GetAllAssessments();

        Task<AssessmentDto> GetAssessmentById(int id);

        Task<int> UpdateAssessment(int id, AssessmentUpdationDto assessment);

        Task<int> DeleteAssessment(int id);

        Task<AssessmentItemDto?> GetAssessmentItemsById(int id);

        Task<IEnumerable<AssessmentDto>> GetAssessmentsByEmployeeId(int employeeId);
    }
}