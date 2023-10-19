using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IAnswerRepository
    {
        Task<int> SaveAnswers(Answer answer);
        Task<IEnumerable<AnswerDto>> GetAnswersByItemId(int itemId);
        Task<AnswerDto> GetAnswersById(int id);
        Task<int> UpdateAnswers(Answer answer);
        Task<int> DeleteAnswers(int id);
        Task<AssessmentAnswersDto?> GetAssessmentAnswersByEmployeeAndAssessmentId(int employeeId, int assessmentId);
    }
}
