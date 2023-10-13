using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> GetEmployeeByUserIdAndTeamId(int userId, int teamId)
        {
            return await _repository.GetEmployeeByUserIdAndTeamId(userId, teamId);
        }

        public async Task<Employee> CreateEmployee(EmployeeCreationDto employee)
        {
            var model = _mapper.Map<Employee>(employee);
            model.Id = await _repository.CreateEmployee(model);

            return model;
        }

        public async Task<EmployeeDto> GetEmployeeByUserIdAndTeamCode(int userId, Guid teamCode)
        {
            return await _repository.GetEmployeeByUserIdAndTeamCode(userId, teamCode);
        }

        public async Task<Employee> CreateEmployeeWithTeamCode(EmployeeTeamInfoDto employee)
        {
            try
            {
                var model = _mapper.Map<Employee>(employee);
                model.Id = await _repository.CreateEmployeeWithTeamCode(employee);

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            return _repository.GetAllEmployees();
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            return await _repository.GetEmployeeById(id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByUserId(int userId)
        {
            return await _repository.GetEmployeeByUserId(userId);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByTeamId(int teamId)
        {
            return await _repository.GetEmployeeByTeamId(teamId);
        }

        public async Task<int> UpdateEmployee(int id, EmployeeUpdationDto employee)
        {
            var model = _mapper.Map<Employee>(employee);
            model.Id = id;

            return await _repository.UpdateEmployee(model);
        }

        public async Task<int> DeleteEmployee(int id)
        {
            return await _repository.DeleteEmployee(id);
        }

        public async Task<EmployeeDetailsDto> GetEmployeeDetailsById(int id)
        {
            return await _repository.GetEmployeeDetailsById(id);
        }
    }
}