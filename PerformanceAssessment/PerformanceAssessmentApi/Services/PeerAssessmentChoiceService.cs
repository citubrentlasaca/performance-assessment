using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class PeerAssessmentChoiceService : IPeerAssessmentChoiceService
    {
        private readonly IPeerAssessmentChoiceRepository _repository;
        private readonly IMapper _mapper;

        public PeerAssessmentChoiceService(IPeerAssessmentChoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PeerAssessmentChoice> CreatePeerAssessmentChoice(PeerAssessmentChoiceCreationDto choice)
        {
            var model = _mapper.Map<PeerAssessmentChoice>(choice);
            model.Id = await _repository.CreatePeerAssessmentChoice(model);

            return model;
        }

        public Task<IEnumerable<PeerAssessmentChoiceDto>> GetAllPeerAssessmentChoices()
        {
            return _repository.GetAllPeerAssessmentChoices();
        }

        public Task<PeerAssessmentChoiceDto> GetPeerAssessmentChoiceById(int id)
        {
            return _repository.GetPeerAssessmentChoiceById(id);
        }

        public async Task<int> UpdatePeerAssessmentChoice(int id, PeerAssessmentChoiceUpdationDto choice)
        {
            var model = _mapper.Map<PeerAssessmentChoice>(choice);
            model.Id = id;

            return await _repository.UpdatePeerAssessmentChoice(model);
        }

        public async Task<int> DeletePeerAssessmentChoice(int id)
        {
            return await _repository.DeletePeerAssessmentChoice(id);
        }
    }
}