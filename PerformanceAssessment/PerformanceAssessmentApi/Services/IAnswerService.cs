using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IAnswerService
    {
        Task<Answer> SaveAnswers(AnswerCreationDto answer);
        Task<IEnumerable<AnswerDto>> GetAnswersByItemId(int itemId);
        Task<AnswerDto> GetAnswersById(int id);
        Task<int> UpdateAnswers(int id, AnswerUpdationDto answer);
        Task<int> DeleteAnswers(int id);
    }
}
