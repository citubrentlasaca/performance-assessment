using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByUserIdAndTeamId(int userId, int teamId);
        Task<int> CreateEmployee(Employee employee);
        Task<EmployeeDto> GetEmployeeByUserIdAndTeamCode(int userId, Guid teamCode);
        Task<int> CreateEmployeeWithTeamCode(EmployeeTeamInfoDto employee);
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> GetEmployeeByUserId(int userId);
        Task<IEnumerable<EmployeeDto>> GetEmployeeByTeamId(int teamId);
        Task<int> UpdateEmployee(Employee employee);
        Task<int> DeleteEmployee(int id);
        Task<EmployeeDetailsDto> GetEmployeeDetailsById(int id);
        Task<EmployeeDto> GetEmployeeByTeamIdAndUserId(int teamId, int userId);
    }
}