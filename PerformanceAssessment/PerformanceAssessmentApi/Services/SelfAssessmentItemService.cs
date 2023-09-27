using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class SelfAssessmentItemService : ISelfAssessmentItemService
    {
        private readonly ISelfAssessmentItemRepository _repository;
        private readonly IMapper _mapper;

        public SelfAssessmentItemService(ISelfAssessmentItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SelfAssessmentItem> CreateSelfAssessmentItem(SelfAssessmentItemCreationDto item)
        {
            var model = _mapper.Map<SelfAssessmentItem>(item);
            model.Id = await _repository.CreateSelfAssessmentItem(model);

            return model;
        }

        public Task<IEnumerable<SelfAssessmentItemChoiceDto>> GetAllSelfAssessmentItems()
        {
            return _repository.GetAllSelfAssessmentItems();
        }

        public Task<SelfAssessmentItemChoiceDto> GetSelfAssessmentItemById(int id)
        {
            return _repository.GetSelfAssessmentItemById(id);
        }

        public async Task<int> UpdateSelfAssessmentItem(int id, SelfAssessmentItemUpdationDto item)
        {
            var model = _mapper.Map<SelfAssessmentItem>(item);
            model.Id = id;

            return await _repository.UpdateSelfAssessmentItem(model);
        }

        public async Task<int> DeleteSelfAssessmentItem(int id)
        {
            return await _repository.DeleteSelfAssessmentItem(id);
        }
    }
}