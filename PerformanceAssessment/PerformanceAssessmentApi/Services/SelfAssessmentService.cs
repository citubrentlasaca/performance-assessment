using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class SelfAssessmentService : ISelfAssessmentService
    {
        private readonly ISelfAssessmentRepository _repository;
        private readonly IMapper _mapper;

        public SelfAssessmentService(ISelfAssessmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SelfAssessment> CreateSelfAssessment(SelfAssessmentCreationDto assessment)
        {
            var model = _mapper.Map<SelfAssessment>(assessment);
            model.Id = await _repository.CreateSelfAssessment(model);

            return model;
        }

        public Task<IEnumerable<SelfAssessmentDto>> GetAllSelfAssessments()
        {
            return _repository.GetAllSelfAssessments();
        }

        public Task<SelfAssessmentDto> GetSelfAssessmentById(int id)
        {
            return _repository.GetSelfAssessmentById(id);
        }

        public async Task<int> UpdateSelfAssessment(int id, SelfAssessmentUpdationDto assessment)
        {
            var model = _mapper.Map<SelfAssessment>(assessment);
            model.Id = id;

            return await _repository.UpdateSelfAssessment(model);
        }

        public async Task<int> DeleteSelfAssessment(int id)
        {
            return await _repository.DeleteSelfAssessment(id);
        }

        public Task<SelfAssessmentItemsDto?> GetSelfAssessmentItemsById(int id)
        {
            return _repository.GetSelfAssessmentItemsById(id);
        }
    }
}