using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IResultRepository
    {
        Task<int> CreateResult(Result result);
        Task<IEnumerable<ResultDto>> GetAllResults();
        Task<ResultDto> GetResultById(int id);
        Task<IEnumerable<ResultDto>> GetResultByAssessmentId(int assessmentId);
        Task<IEnumerable<ResultDto>> GetResultByEmployeeId(int employeeId);
        Task<int> UpdateResult(Result result);
        Task<int> DeleteResult(int id);
    }
}
