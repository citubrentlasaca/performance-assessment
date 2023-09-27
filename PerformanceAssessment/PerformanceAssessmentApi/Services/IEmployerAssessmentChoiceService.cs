using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployerAssessmentChoiceService
    {
        Task<EmployerAssessmentChoice> CreateEmployerAssessmentChoice(EmployerAssessmentChoiceCreationDto choice);

        Task<IEnumerable<EmployerAssessmentChoiceDto>> GetAllEmployerAssessmentChoices();

        Task<EmployerAssessmentChoiceDto> GetEmployerAssessmentChoiceById(int id);

        Task<int> UpdateEmployerAssessmentChoice(int id, EmployerAssessmentChoiceUpdationDto choice);

        Task<int> DeleteEmployerAssessmentChoice(int id);
    }
}
