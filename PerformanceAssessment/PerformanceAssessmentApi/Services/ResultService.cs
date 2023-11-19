using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _repository;
        private readonly IMapper _mapper;

        public ResultService(IResultRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> CreateResult(ResultCreationDto result)
        {
            var model = _mapper.Map<Result>(result);
            model.Id = await _repository.CreateResult(model);

            return model;
        }

        public Task<IEnumerable<ResultDto>> GetAllResults()
        {
            return _repository.GetAllResults();
        }

        public Task<ResultDto> GetResultById(int id)
        {
            return _repository.GetResultById(id);
        }

        public Task<IEnumerable<ResultDto>> GetResultByAssessmentId(int assessmentId)
        {
            return _repository.GetResultByAssessmentId(assessmentId);
        }

        public Task<IEnumerable<ResultDto>> GetResultByEmployeeId(int employeeId)
        {
            return _repository.GetResultByEmployeeId(employeeId);
        }

        public async Task<int> UpdateResult(int id, ResultUpdationDto schedule)
        {
            var model = _mapper.Map<Result>(schedule);
            model.Id = id;

            return await _repository.UpdateResult(model);
        }

        public async Task<int> DeleteResult(int id)
        {
            return await _repository.DeleteResult(id);
        }
    }
}