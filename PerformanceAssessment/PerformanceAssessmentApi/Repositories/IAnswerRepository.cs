using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<int>> SaveAnswers(IEnumerable<int> resultIds, Answer answer);
        Task<IEnumerable<AnswerDto>> GetAnswersByItemId(int itemId);
        Task<AnswerDto> GetAnswersById(int id);
        Task<int> UpdateAnswers(Answer answer);
        Task<int> DeleteAnswers(int id);
        Task<AssessmentAnswersDto> GetAssessmentAnswersByEmployeeIdAndAssessmentId(int employeeId, int assessmentId);
    }
}
