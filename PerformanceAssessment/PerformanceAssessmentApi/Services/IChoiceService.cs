using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IChoiceService
    {
        Task<Choice> CreateChoice(ChoiceCreationDto choice);

        Task<IEnumerable<ChoiceDto>> GetAllChoices();

        Task<ChoiceDto> GetChoiceById(int id);

        Task<int> UpdateChoice(int id, ChoiceUpdationDto choice);

        Task<int> DeleteChoice(int id);
    }
}