using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class SelfAssessmentChoiceService : ISelfAssessmentChoiceService
    {
        private readonly ISelfAssessmentChoiceRepository _repository;
        private readonly IMapper _mapper;

        public SelfAssessmentChoiceService(ISelfAssessmentChoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SelfAssessmentChoice> CreateSelfAssessmentChoice(SelfAssessmentChoiceCreationDto choice)
        {
            var model = _mapper.Map<SelfAssessmentChoice>(choice);
            model.Id = await _repository.CreateSelfAssessmentChoice(model);

            return model;
        }

        public Task<IEnumerable<SelfAssessmentChoiceDto>> GetAllSelfAssessmentChoices()
        {
            return _repository.GetAllSelfAssessmentChoices();
        }

        public Task<SelfAssessmentChoiceDto> GetSelfAssessmentChoiceById(int id)
        {
            return _repository.GetSelfAssessmentChoiceById(id);
        }

        public async Task<int> UpdateSelfAssessmentChoice(int id, SelfAssessmentChoiceUpdationDto choice)
        {
            var model = _mapper.Map<SelfAssessmentChoice>(choice);
            model.Id = id;

            return await _repository.UpdateSelfAssessmentChoice(model);
        }

        public async Task<int> DeleteSelfAssessmentChoice(int id)
        {
            return await _repository.DeleteSelfAssessmentChoice(id);
        }
    }
}