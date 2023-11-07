using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _repository;
        private readonly IMapper _mapper;

        public AnalyticsService(IAnalyticsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentId(int assessmentId)
        {
            return await _repository.GetEmployeesPerformanceByAssessmentId(assessmentId);
        }

        public async Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndMonthNumber(int assessmentId, int monthNumber)
        {
            return await _repository.GetEmployeesPerformanceByAssessmentIdAndMonthNumber(assessmentId, monthNumber);
        }

        public async Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndYear(int assessmentId, int year)
        {
            return await _repository.GetEmployeesPerformanceByAssessmentIdAndYear(assessmentId, year);
        }
    }
}