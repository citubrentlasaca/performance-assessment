using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IItemRepository
    {
        Task<int> CreateItem(Item item);

        Task<IEnumerable<ItemChoiceDto>> GetAllItems();

        Task<ItemChoiceDto> GetItemById(int id);

        Task<int> UpdateItem(Item item);

        Task<int> DeleteItem(int id);
    }
}