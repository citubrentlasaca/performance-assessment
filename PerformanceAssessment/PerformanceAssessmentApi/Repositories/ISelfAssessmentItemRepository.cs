using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface ISelfAssessmentItemRepository
    {
        Task<int> CreateSelfAssessmentItem(SelfAssessmentItem item);

        Task<IEnumerable<SelfAssessmentItemChoiceDto>> GetAllSelfAssessmentItems();

        Task<SelfAssessmentItemChoiceDto> GetSelfAssessmentItemById(int id);

        Task<int> UpdateSelfAssessmentItem(SelfAssessmentItem item);

        Task<int> DeleteSelfAssessmentItem(int id);
    }
}