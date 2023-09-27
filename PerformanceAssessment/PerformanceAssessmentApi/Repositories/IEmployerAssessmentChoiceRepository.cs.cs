using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployerAssessmentChoiceRepository
    {
        Task<int> CreateEmployerAssessmentChoice(EmployerAssessmentChoice choice);

        Task<IEnumerable<EmployerAssessmentChoiceDto>> GetAllEmployerAssessmentChoices();

        Task<EmployerAssessmentChoiceDto> GetEmployerAssessmentChoiceById(int id);

        Task<int> UpdateEmployerAssessmentChoice(EmployerAssessmentChoice choice);

        Task<int> DeleteEmployerAssessmentChoice(int id);
    }
}
