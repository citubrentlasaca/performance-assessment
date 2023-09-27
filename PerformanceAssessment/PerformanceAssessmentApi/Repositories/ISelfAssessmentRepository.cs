using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface ISelfAssessmentRepository
    {
        Task<int> CreateSelfAssessment(SelfAssessment assessment);

        Task<IEnumerable<SelfAssessmentDto>> GetAllSelfAssessments();

        Task<SelfAssessmentDto> GetSelfAssessmentById(int id);

        Task<int> UpdateSelfAssessment(SelfAssessment assessment);

        Task<int> DeleteSelfAssessment(int id);

        Task<SelfAssessmentItemsDto?> GetSelfAssessmentItemsById(int id);
    }
}