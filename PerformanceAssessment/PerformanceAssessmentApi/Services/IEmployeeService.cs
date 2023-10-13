using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployeeByUserIdAndTeamId(int userId, int teamId);
        Task<Employee> CreateEmployee(EmployeeCreationDto employee);
        Task<EmployeeDto> GetEmployeeByUserIdAndTeamCode(int userId, Guid teamCode);
        Task<Employee> CreateEmployeeWithTeamCode(EmployeeTeamInfoDto employee);
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployeeByUserId(int userId);
        Task<IEnumerable<EmployeeDto>> GetEmployeeByTeamId(int teamId);
        Task<int> UpdateEmployee(int id, EmployeeUpdationDto employee);
        Task<int> DeleteEmployee(int id);
        Task<EmployeeDetailsDto> GetEmployeeDetailsById(int id);
    }
}
