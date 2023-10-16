using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IResultService
    {
        Task<Result> CreateResult(ResultCreationDto result);
        Task<IEnumerable<ResultDto>> GetAllResults();
        Task<ResultDto> GetResultById(int id);
        Task<IEnumerable<ResultDto>> GetResultByAssessmentId(int assessmentId);
        Task<IEnumerable<ResultDto>> GetResultByEmployeeId(int employeeId);
        Task<int> UpdateResult(int id, ResultUpdationDto result);
        Task<int> DeleteResult(int id);
    }
}
