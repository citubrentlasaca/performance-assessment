using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployerAssessmentItemService
    {
        Task<EmployerAssessmentItem> CreateEmployerAssessmentItem(EmployerAssessmentItemCreationDto item);

        Task<IEnumerable<EmployerAssessmentItemChoiceDto>> GetAllEmployerAssessmentItems();

        Task<EmployerAssessmentItemChoiceDto> GetEmployerAssessmentItemById(int id);

        Task<int> UpdateEmployerAssessmentItem(int id, EmployerAssessmentItemUpdationDto item);

        Task<int> DeleteEmployerAssessmentItem(int id);
    }
}
