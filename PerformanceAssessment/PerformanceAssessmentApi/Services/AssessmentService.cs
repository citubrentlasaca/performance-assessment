using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _repository;
        private readonly IMapper _mapper;

        public AssessmentService(IAssessmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Assessment> CreateAssessment(AssessmentCreationDto assessment)
        {
            var model = _mapper.Map<Assessment>(assessment);
            model.Id = await _repository.CreateAssessment(model);

            return model;
        }

        public Task<IEnumerable<AssessmentDto>> GetAllAssessments()
        {
            return _repository.GetAllAssessments();
        }

        public Task<AssessmentDto> GetAssessmentById(int id)
        {
            return _repository.GetAssessmentById(id);
        }

        public async Task<int> UpdateAssessment(int id, AssessmentUpdationDto assessment)
        {
            var model = _mapper.Map<Assessment>(assessment);
            model.Id = id;

            return await _repository.UpdateAssessment(model);
        }

        public async Task<int> DeleteAssessment(int id)
        {
            return await _repository.DeleteAssessment(id);
        }

        public Task<AssessmentItemDto?> GetAssessmentItemsById(int id)
        {
            return _repository.GetAssessmentItemsById(id);
        }
    }
}