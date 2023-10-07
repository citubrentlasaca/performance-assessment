using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<int> CreateEmployee(Employee employee);
        Task<int> CreateEmployeeWithTeamCode(EmployeeTeamInfoDto employee);
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> GetEmployeeByUserId(int userId);
        Task<EmployeeDto> GetEmployeeByTeamId(int teamId);
        Task<int> UpdateEmployee(Employee employee);
        Task<int> DeleteEmployee(int id);
        Task<EmployeeDetailsDto> GetEmployeeDetailsById(int id);
    }
}