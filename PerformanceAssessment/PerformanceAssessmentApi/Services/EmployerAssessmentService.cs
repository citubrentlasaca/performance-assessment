using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class EmployerAssessmentService : IEmployerAssessmentService
    {
        private readonly IEmployerAssessmentRepository _repository;
        private readonly IMapper _mapper;

        public EmployerAssessmentService(IEmployerAssessmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployerAssessment> CreateEmployerAssessment(EmployerAssessmentCreationDto assessment)
        {
            var model = _mapper.Map<EmployerAssessment>(assessment);
            model.Id = await _repository.CreateEmployerAssessment(model);

            return model;
        }

        public Task<IEnumerable<EmployerAssessmentDto>> GetAllEmployerAssessments()
        {
            return _repository.GetAllEmployerAssessments();
        }

        public Task<EmployerAssessmentDto> GetEmployerAssessmentById(int id)
        {
            return _repository.GetEmployerAssessmentById(id);
        }

        public async Task<int> UpdateEmployerAssessment(int id, EmployerAssessmentUpdationDto assessment)
        {
            var model = _mapper.Map<EmployerAssessment>(assessment);
            model.Id = id;

            return await _repository.UpdateEmployerAssessment(model);
        }

        public async Task<int> DeleteEmployerAssessment(int id)
        {
            return await _repository.DeleteEmployerAssessment(id);
        }

        public Task<EmployerAssessmentItemsDto?> GetEmployerAssessmentItemsById(int id)
        {
            return _repository.GetEmployerAssessmentItemsById(id);
        }
    }
}