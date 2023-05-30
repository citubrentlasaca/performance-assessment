using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IItemService
    {
        Task<Item> CreateItem(ItemCreationDto item);

        Task<IEnumerable<ItemChoiceDto>> GetAllItems();

        Task<ItemChoiceDto> GetItemById(int id);

        Task<int> UpdateItem(int id, ItemUpdationDto item);

        Task<int> DeleteItem(int id);
    }
}