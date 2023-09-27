using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class PeerAssessmentItemService : IPeerAssessmentItemService
    {
        private readonly IPeerAssessmentItemRepository _repository;
        private readonly IMapper _mapper;

        public PeerAssessmentItemService(IPeerAssessmentItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PeerAssessmentItem> CreatePeerAssessmentItem(PeerAssessmentItemCreationDto item)
        {
            var model = _mapper.Map<PeerAssessmentItem>(item);
            model.Id = await _repository.CreatePeerAssessmentItem(model);

            return model;
        }

        public Task<IEnumerable<PeerAssessmentItemChoiceDto>> GetAllPeerAssessmentItems()
        {
            return _repository.GetAllPeerAssessmentItems();
        }

        public Task<PeerAssessmentItemChoiceDto> GetPeerAssessmentItemById(int id)
        {
            return _repository.GetPeerAssessmentItemById(id);
        }

        public async Task<int> UpdatePeerAssessmentItem(int id, PeerAssessmentItemUpdationDto item)
        {
            var model = _mapper.Map<PeerAssessmentItem>(item);
            model.Id = id;

            return await _repository.UpdatePeerAssessmentItem(model);
        }

        public async Task<int> DeletePeerAssessmentItem(int id)
        {
            return await _repository.DeletePeerAssessmentItem(id);
        }
    }
}