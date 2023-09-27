using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployerAssessmentItemRepository
    {
        Task<int> CreateEmployerAssessmentItem(EmployerAssessmentItem item);

        Task<IEnumerable<EmployerAssessmentItemChoiceDto>> GetAllEmployerAssessmentItems();

        Task<EmployerAssessmentItemChoiceDto> GetEmployerAssessmentItemById(int id);

        Task<int> UpdateEmployerAssessmentItem(EmployerAssessmentItem item);

        Task<int> DeleteEmployerAssessmentItem(int id);
    }
}
