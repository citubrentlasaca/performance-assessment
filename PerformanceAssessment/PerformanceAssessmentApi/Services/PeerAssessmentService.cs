using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class PeerAssessmentService : IPeerAssessmentService
    {
        private readonly IPeerAssessmentRepository _repository;
        private readonly IMapper _mapper;

        public PeerAssessmentService(IPeerAssessmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PeerAssessment> CreatePeerAssessment(PeerAssessmentCreationDto assessment)
        {
            var model = _mapper.Map<PeerAssessment>(assessment);
            model.Id = await _repository.CreatePeerAssessment(model);

            return model;
        }

        public Task<IEnumerable<PeerAssessmentDto>> GetAllPeerAssessments()
        {
            return _repository.GetAllPeerAssessments();
        }

        public Task<PeerAssessmentDto> GetPeerAssessmentById(int id)
        {
            return _repository.GetPeerAssessmentById(id);
        }

        public async Task<int> UpdatePeerAssessment(int id, PeerAssessmentUpdationDto assessment)
        {
            var model = _mapper.Map<PeerAssessment>(assessment);
            model.Id = id;

            return await _repository.UpdatePeerAssessment(model);
        }

        public async Task<int> DeletePeerAssessment(int id)
        {
            return await _repository.DeletePeerAssessment(id);
        }

        public Task<PeerAssessmentItemsDto?> GetPeerAssessmentItemsById(int id)
        {
            return _repository.GetPeerAssessmentItemsById(id);
        }
    }
}