using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class EmployerAssessmentChoiceService : IEmployerAssessmentChoiceService
    {
        private readonly IEmployerAssessmentChoiceRepository _repository;
        private readonly IMapper _mapper;

        public EmployerAssessmentChoiceService(IEmployerAssessmentChoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployerAssessmentChoice> CreateEmployerAssessmentChoice(EmployerAssessmentChoiceCreationDto choice)
        {
            var model = _mapper.Map<EmployerAssessmentChoice>(choice);
            model.Id = await _repository.CreateEmployerAssessmentChoice(model);

            return model;
        }

        public Task<IEnumerable<EmployerAssessmentChoiceDto>> GetAllEmployerAssessmentChoices()
        {
            return _repository.GetAllEmployerAssessmentChoices();
        }

        public Task<EmployerAssessmentChoiceDto> GetEmployerAssessmentChoiceById(int id)
        {
            return _repository.GetEmployerAssessmentChoiceById(id);
        }

        public async Task<int> UpdateEmployerAssessmentChoice(int id, EmployerAssessmentChoiceUpdationDto choice)
        {
            var model = _mapper.Map<EmployerAssessmentChoice>(choice);
            model.Id = id;

            return await _repository.UpdateEmployerAssessmentChoice(model);
        }

        public async Task<int> DeleteEmployerAssessmentChoice(int id)
        {
            return await _repository.DeleteEmployerAssessmentChoice(id);
        }
    }
}