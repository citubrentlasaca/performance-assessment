using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class EmployerAssessmentItemService : IEmployerAssessmentItemService
    {
        private readonly IEmployerAssessmentItemRepository _repository;
        private readonly IMapper _mapper;

        public EmployerAssessmentItemService(IEmployerAssessmentItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployerAssessmentItem> CreateEmployerAssessmentItem(EmployerAssessmentItemCreationDto item)
        {
            var model = _mapper.Map<EmployerAssessmentItem>(item);
            model.Id = await _repository.CreateEmployerAssessmentItem(model);

            return model;
        }

        public Task<IEnumerable<EmployerAssessmentItemChoiceDto>> GetAllEmployerAssessmentItems()
        {
            return _repository.GetAllEmployerAssessmentItems();
        }

        public Task<EmployerAssessmentItemChoiceDto> GetEmployerAssessmentItemById(int id)
        {
            return _repository.GetEmployerAssessmentItemById(id);
        }

        public async Task<int> UpdateEmployerAssessmentItem(int id, EmployerAssessmentItemUpdationDto item)
        {
            var model = _mapper.Map<EmployerAssessmentItem>(item);
            model.Id = id;

            return await _repository.UpdateEmployerAssessmentItem(model);
        }

        public async Task<int> DeleteEmployerAssessmentItem(int id)
        {
            return await _repository.DeleteEmployerAssessmentItem(id);
        }
    }
}