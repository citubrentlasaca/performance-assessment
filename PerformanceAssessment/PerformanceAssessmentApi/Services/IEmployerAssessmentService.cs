using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployerAssessmentService
    {
        Task<EmployerAssessment> CreateEmployerAssessment(EmployerAssessmentCreationDto assessment);

        Task<IEnumerable<EmployerAssessmentDto>> GetAllEmployerAssessments();

        Task<EmployerAssessmentDto> GetEmployerAssessmentById(int id);

        Task<int> UpdateEmployerAssessment(int id, EmployerAssessmentUpdationDto assessment);

        Task<int> DeleteEmployerAssessment(int id);

        Task<EmployerAssessmentItemsDto?> GetEmployerAssessmentItemsById(int id);
    }
}
