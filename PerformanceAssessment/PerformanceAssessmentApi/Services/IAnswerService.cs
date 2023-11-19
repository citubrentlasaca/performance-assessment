using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IAnswerService
    {
        Task<IEnumerable<int>> SaveAnswers(IEnumerable<int> resultIds, AnswerCreationDto answerCreation);
        Task<IEnumerable<AnswerDto>> GetAnswersByItemId(int itemId);
        Task<AnswerDto> GetAnswersById(int id);
        Task<int> UpdateAnswers(int id, AnswerUpdationDto answer);
        Task<int> DeleteAnswers(int id);
        Task<AssessmentAnswersDto> GetAssessmentAnswersByEmployeeIdAndAssessmentId(int employeeId, int assessmentId);
    }
}
