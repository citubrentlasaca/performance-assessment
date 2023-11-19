using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _repository;
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<int>> SaveAnswers(IEnumerable<int> resultIds, AnswerCreationDto answerCreation)
        {
            var answers = _mapper.Map<Answer>(answerCreation);
            var insertedIds = await _repository.SaveAnswers(resultIds, answers);

            return insertedIds;
        }

        public async Task<IEnumerable<AnswerDto>> GetAnswersByItemId(int itemId)
        {
            return await _repository.GetAnswersByItemId(itemId);
        }

        public async Task<AnswerDto> GetAnswersById(int id)
        {
            return await _repository.GetAnswersById(id);
        }

        public async Task<int> UpdateAnswers(int id, AnswerUpdationDto answer)
        {
            var model = _mapper.Map<Answer>(answer);
            model.Id = id;

            return await _repository.UpdateAnswers(model);
        }

        public async Task<int> DeleteAnswers(int id)
        {
            return await _repository.DeleteAnswers(id);
        }

        public async Task<AssessmentAnswersDto> GetAssessmentAnswersByEmployeeIdAndAssessmentId(int employeeId, int assessmentId)
        {
            return await _repository.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId);
        }
    }
}