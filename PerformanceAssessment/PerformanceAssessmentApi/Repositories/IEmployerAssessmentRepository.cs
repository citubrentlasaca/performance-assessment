using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployerAssessmentRepository
    {
        Task<int> CreateEmployerAssessment(EmployerAssessment assessment);

        Task<IEnumerable<EmployerAssessmentDto>> GetAllEmployerAssessments();

        Task<EmployerAssessmentDto> GetEmployerAssessmentById(int id);

        Task<int> UpdateEmployerAssessment(EmployerAssessment assessment);

        Task<int> DeleteEmployerAssessment(int id);

        Task<EmployerAssessmentItemsDto?> GetEmployerAssessmentItemsById(int id);
    }
}
