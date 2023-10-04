using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployee(EmployeeCreationDto employee);
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> GetEmployeeByUserId(int userId);
        Task<EmployeeDto> GetEmployeeByTeamId(int teamId);
        Task<int> UpdateEmployee(int id, EmployeeUpdationDto employee);
        Task<int> DeleteEmployee(int id);
        Task<EmployeeDetailsDto> GetEmployeeDetailsById(int id);
    }
}
