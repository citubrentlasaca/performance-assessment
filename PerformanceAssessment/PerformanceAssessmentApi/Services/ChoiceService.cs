using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class ChoiceService : IChoiceService
    {
        private readonly IChoiceRepository _repository;
        private readonly IMapper _mapper;

        public ChoiceService(IChoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Choice> CreateChoice(ChoiceCreationDto choice)
        {
            var model = _mapper.Map<Choice>(choice);
            model.Id = await _repository.CreateChoice(model);

            return model;
        }

        public Task<IEnumerable<ChoiceDto>> GetAllChoices()
        {
            return _repository.GetAllChoices();
        }

        public Task<ChoiceDto> GetChoiceById(int id)
        {
            return _repository.GetChoiceById(id);
        }

        public async Task<int> UpdateChoice(int id, ChoiceUpdationDto choice)
        {
            var model = _mapper.Map<Choice>(choice);
            model.Id = id;

            return await _repository.UpdateChoice(model);
        }

        public async Task<int> DeleteChoice(int id)
        {
            return await _repository.DeleteChoice(id);
        }
    }
}