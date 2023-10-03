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

        public async Task<Answer> SaveAnswers(AnswerCreationDto answer)
        {
            var model = _mapper.Map<Answer>(answer);
            model.Id = await _repository.SaveAnswers(model);

            return model;
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
    }
}