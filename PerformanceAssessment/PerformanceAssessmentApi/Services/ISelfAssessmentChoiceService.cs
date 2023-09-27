using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface ISelfAssessmentChoiceService
    {
        Task<SelfAssessmentChoice> CreateSelfAssessmentChoice(SelfAssessmentChoiceCreationDto choice);

        Task<IEnumerable<SelfAssessmentChoiceDto>> GetAllSelfAssessmentChoices();

        Task<SelfAssessmentChoiceDto> GetSelfAssessmentChoiceById(int id);

        Task<int> UpdateSelfAssessmentChoice(int id, SelfAssessmentChoiceUpdationDto choice);

        Task<int> DeleteSelfAssessmentChoice(int id);
    }
}