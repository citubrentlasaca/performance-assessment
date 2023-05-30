using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Item> CreateItem(ItemCreationDto item)
        {
            var model = _mapper.Map<Item>(item);
            model.Id = await _repository.CreateItem(model);

            return model;
        }

        public Task<IEnumerable<ItemChoiceDto>> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public Task<ItemChoiceDto> GetItemById(int id)
        {
            return _repository.GetItemById(id);
        }

        public async Task<int> UpdateItem(int id, ItemUpdationDto item)
        {
            var model = _mapper.Map<Item>(item);
            model.Id = id;

            return await _repository.UpdateItem(model);
        }

        public async Task<int> DeleteItem(int id)
        {
            return await _repository.DeleteItem(id);
        }
    }
}