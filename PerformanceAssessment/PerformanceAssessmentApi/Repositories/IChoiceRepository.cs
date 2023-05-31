using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IChoiceRepository
    {
        Task<int> CreateChoice(Choice choice);

        Task<IEnumerable<ChoiceDto>> GetAllChoices();

        Task<ChoiceDto> GetChoiceById(int id);

        Task<int> UpdateChoice(Choice choice);

        Task<int> DeleteChoice(int id);
    }
}