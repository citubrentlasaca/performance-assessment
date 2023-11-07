using PerformanceAssessmentApi.Dtos;

namespace PerformanceAssessmentApi.Services
{
    public interface IAnalyticsService
    {
        Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentId(int assessmentId);
        Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndMonthNumber(int assessmentId, int monthNumber);
        Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndYear(int assessmentId, int year);
    }
}