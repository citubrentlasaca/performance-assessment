using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface ISelfAssessmentService
    {
        Task<SelfAssessment> CreateSelfAssessment(SelfAssessmentCreationDto assessment);

        Task<IEnumerable<SelfAssessmentDto>> GetAllSelfAssessments();

        Task<SelfAssessmentDto> GetSelfAssessmentById(int id);

        Task<int> UpdateSelfAssessment(int id, SelfAssessmentUpdationDto assessment);

        Task<int> DeleteSelfAssessment(int id);

        Task<SelfAssessmentItemsDto?> GetSelfAssessmentItemsById(int id);
    }
}