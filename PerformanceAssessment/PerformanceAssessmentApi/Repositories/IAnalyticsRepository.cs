using PerformanceAssessmentApi.Dtos;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IAnalyticsRepository
    {
        Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentId(int assessmentId);
        Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndMonthNumber(int assessmentId, int monthNumber);
        Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndYear(int assessmentId, int year);
    }
}