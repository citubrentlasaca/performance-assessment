using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface ISelfAssessmentItemService
    {
        Task<SelfAssessmentItem> CreateSelfAssessmentItem(SelfAssessmentItemCreationDto item);

        Task<IEnumerable<SelfAssessmentItemChoiceDto>> GetAllSelfAssessmentItems();

        Task<SelfAssessmentItemChoiceDto> GetSelfAssessmentItemById(int id);

        Task<int> UpdateSelfAssessmentItem(int id, SelfAssessmentItemUpdationDto item);

        Task<int> DeleteSelfAssessmentItem(int id);
    }
}