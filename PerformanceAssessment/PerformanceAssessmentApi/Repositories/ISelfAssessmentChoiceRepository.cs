using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface ISelfAssessmentChoiceRepository
    {
        Task<int> CreateSelfAssessmentChoice(SelfAssessmentChoice choice);

        Task<IEnumerable<SelfAssessmentChoiceDto>> GetAllSelfAssessmentChoices();

        Task<SelfAssessmentChoiceDto> GetSelfAssessmentChoiceById(int id);

        Task<int> UpdateSelfAssessmentChoice(SelfAssessmentChoice choice);

        Task<int> DeleteSelfAssessmentChoice(int id);
    }
}