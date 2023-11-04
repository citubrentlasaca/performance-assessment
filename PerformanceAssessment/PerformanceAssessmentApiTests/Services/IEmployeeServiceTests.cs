using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IEmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _fakeEmployeeRepository;
        private readonly Mock<ITeamRepository> _fakeTeamRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IEmployeeService _employeeService;

        public IEmployeeServiceTests()
        {
            _fakeEmployeeRepository = new Mock<IEmployeeRepository>();
            _fakeTeamRepository = new Mock<ITeamRepository>();
            _fakeMapper = new Mock<IMapper>();
            _employeeService = new EmployeeService(_fakeEmployeeRepository.Object, _fakeTeamRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task GetEmployeeByUserIdAndTeamId_ExistingEmployee_ReturnsEmployee()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var expectedEmployee = new Employee
            {
                Id = employeeId,
                UserId = userId,
                TeamId = teamId,
                Role = "User",
                Status = "Active"
            };

            _fakeEmployeeRepository.Setup(x => x.GetEmployeeByUserIdAndTeamId(userId, teamId)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _employeeService.GetEmployeeByUserIdAndTeamId(userId, teamId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEmployee.Id, result.Id);
            Assert.Equal(expectedEmployee.UserId, result.UserId);
            Assert.Equal(expectedEmployee.TeamId, result.TeamId);
            Assert.Equal(expectedEmployee.Role, result.Role);
            Assert.Equal(expectedEmployee.Status, result.Status);
        }

        [Fact]
        public async Task CreateEmployee_ValidEmployeeCreationDto_ReturnsCreatedEmployee()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeCreationDto = new EmployeeCreationDto
            {
                UserId = userId,
                TeamId = teamId
            };

            var employee = new Employee
            {
                Id = employeeId,
                UserId = userId,
                TeamId = teamId,
                Role = "User",
                Status = "Active"
            };

            _fakeMapper.Setup(x => x.Map<Employee>(employeeCreationDto)).Returns(employee);
            _fakeEmployeeRepository.Setup(x => x.CreateEmployee(employee)).ReturnsAsync(employee.Id);

            // Act
            var createdEmployee = await _employeeService.CreateEmployee(employeeCreationDto);

            // Assert
            Assert.NotNull(createdEmployee);
            Assert.Equal(employee.Id, createdEmployee.Id);
            Assert.Equal(employee.UserId, createdEmployee.UserId);
            Assert.Equal(employee.TeamId, createdEmployee.TeamId);
            Assert.Equal(employee.Role, createdEmployee.Role);
            Assert.Equal(employee.Status, createdEmployee.Status);
        }

        [Fact]
        public async Task GetEmployeeByUserIdAndTeamCode_ExistingEmployee_ReturnsEmployeeDto()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();
            Guid teamCode = Guid.NewGuid();

            var expectedEmployeeDto = new EmployeeDto
            {
                Id = employeeId,
                UserId = userId,
                TeamId = teamId,
                Role = "User",
                Status = "Active"
            };

            _fakeEmployeeRepository.Setup(x => x.GetEmployeeByUserIdAndTeamCode(userId, teamCode)).ReturnsAsync(expectedEmployeeDto);

            // Act
            var result = await _employeeService.GetEmployeeByUserIdAndTeamCode(userId, teamCode);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEmployeeDto.Id, result.Id);
            Assert.Equal(expectedEmployeeDto.UserId, result.UserId);
            Assert.Equal(expectedEmployeeDto.TeamId, result.TeamId);
            Assert.Equal(expectedEmployeeDto.Role, result.Role);
            Assert.Equal(expectedEmployeeDto.Status, result.Status);
        }

        [Fact]
        public async Task CreateEmployeeWithTeamCode_ValidEmployeeTeamInfoDto_ReturnsCreatedEmployee()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();
            var teamCode = Guid.NewGuid();

            var employeeTeamInfoDto = new EmployeeTeamInfoDto
            {
                UserId = userId,
                TeamCode = teamCode,
                Role = "User",
                Status = "Active"
            };

            var employee = new Employee
            {
                Id = employeeId,
                UserId = userId,
                TeamId = teamId,
                Role = "User",
                Status = "Active"
            };

            _fakeMapper.Setup(x => x.Map<Employee>(employeeTeamInfoDto)).Returns(employee);
            _fakeTeamRepository.Setup(x => x.GetTeamByCode(teamCode)).ReturnsAsync(new TeamDto { Id = teamId });
            _fakeEmployeeRepository.Setup(x => x.GetEmployeeByUserIdAndTeamCode(userId, teamCode)).ReturnsAsync(new EmployeeDto { Id = employeeId });
            _fakeEmployeeRepository.Setup(x => x.CreateEmployeeWithTeamCode(employeeTeamInfoDto)).ReturnsAsync(employee.Id);

            // Act
            var createdEmployee = await _employeeService.CreateEmployeeWithTeamCode(employeeTeamInfoDto);

            // Assert
            Assert.NotNull(createdEmployee);
            Assert.Equal(employee.Id, createdEmployee.Id);
            Assert.Equal(employee.UserId, createdEmployee.UserId);
            Assert.Equal(employee.TeamId, createdEmployee.TeamId);
            Assert.Equal(employee.Role, createdEmployee.Role);
            Assert.Equal(employee.Status, createdEmployee.Status);
        }

        [Fact]
        public async Task GetAllEmployees_ExistingEmployees_ReturnsAllEmployees()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employees = new List<EmployeeDto>
            {
                new EmployeeDto
                {
                    Id = employeeId,
                    UserId = userId,
                    TeamId = teamId,
                    Role = "User",
                    Status = "Active"
                },
                new EmployeeDto
                {
                    Id = employeeId,
                    UserId = userId,
                    TeamId = teamId,
                    Role = "Admin",
                    Status = "Active"
                },
            };

            _fakeEmployeeRepository.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetAllEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employees.Count, result.Count());
        }

        [Fact]
        public async Task GetEmployeeById_ExistingEmployee_ReturnsEmployeeDto()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var expectedEmployeeDto = new EmployeeDto
            {
                Id = employeeId,
                UserId = userId,
                TeamId = teamId,
                Role = "User",
                Status = "Active"
            };

            _fakeEmployeeRepository.Setup(x => x.GetEmployeeById(employeeId)).ReturnsAsync(expectedEmployeeDto);

            // Act
            var result = await _employeeService.GetEmployeeById(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEmployeeDto.Id, result.Id);
            Assert.Equal(expectedEmployeeDto.UserId, result.UserId);
            Assert.Equal(expectedEmployeeDto.TeamId, result.TeamId);
            Assert.Equal(expectedEmployeeDto.Role, result.Role);
            Assert.Equal(expectedEmployeeDto.Status, result.Status);
        }

        [Fact]
        public async Task GetEmployeeByUserId_ExistingEmployees_ReturnsEmployeeDtos()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employees = new List<EmployeeDto>
            {
                new EmployeeDto
                {
                    Id = employeeId,
                    UserId = userId,
                    TeamId = teamId,
                    Role = "User",
                    Status = "Active"
                },
                new EmployeeDto
                {
                    Id = employeeId,
                    UserId = userId,
                    TeamId = teamId,
                    Role = "Admin",
                    Status = "Active"
                },
            };

            _fakeEmployeeRepository.Setup(x => x.GetEmployeeByUserId(userId)).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetEmployeeByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employees.Count, result.Count());
        }

        [Fact]
        public async Task GetEmployeeByTeamId_ExistingEmployees_ReturnsEmployeeDtos()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employees = new List<EmployeeDto>
            {
                new EmployeeDto
                {
                    Id = employeeId,
                    UserId = userId,
                    TeamId = teamId,
                    Role = "User",
                    Status = "Active"
                },
                new EmployeeDto
                {
                    Id = employeeId,
                    UserId = userId,
                    TeamId = teamId,
                    Role = "Admin",
                    Status = "Active"
                },
            };

            _fakeEmployeeRepository.Setup(x => x.GetEmployeeByTeamId(teamId)).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetEmployeeByTeamId(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employees.Count, result.Count());
        }

        [Fact]
        public async Task UpdateEmployee_ExistingEmployee_ReturnsNumberOfUpdatedEmployees()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeUpdationDto = new EmployeeUpdationDto
            {
                UserId = userId,
                TeamId = teamId,
                Role = "Admin",
                Status = "Inactive"
            };

            var updatedEmployee = new Employee
            {
                Id = employeeId,
                UserId = userId,
                TeamId = teamId,
                Role = "Admin",
                Status = "Inactive"
            };

            _fakeMapper.Setup(x => x.Map<Employee>(employeeUpdationDto)).Returns(updatedEmployee);
            _fakeEmployeeRepository.Setup(x => x.UpdateEmployee(updatedEmployee)).ReturnsAsync(1);

            // Act
            var result = await _employeeService.UpdateEmployee(employeeId, employeeUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteEmployee_ExistingEmployee_ReturnsNumberOfDeletedEmployees()
        {
            // Arrange
            int employeeId = It.IsAny<int>();

            _fakeEmployeeRepository.Setup(x => x.DeleteEmployee(employeeId)).ReturnsAsync(1);

            // Act
            var result = await _employeeService.DeleteEmployee(employeeId);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetEmployeeDetailsById_ExistingEmployee_ReturnsEmployeeDetailsDto()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            var expectedEmployeeDetailsDto = new EmployeeDetailsDto
            {
                Id = employeeId,
                Role = "User",
                Status = "Active"
            };

            _fakeEmployeeRepository.Setup(x => x.GetEmployeeDetailsById(employeeId)).ReturnsAsync(expectedEmployeeDetailsDto);

            // Act
            var result = await _employeeService.GetEmployeeDetailsById(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEmployeeDetailsDto.Id, result.Id);
            Assert.Equal(expectedEmployeeDetailsDto.Role, result.Role);
            Assert.Equal(expectedEmployeeDetailsDto.Status, result.Status);
        }

        [Fact]
        public async Task GetEmployeeByTeamIdAndUserId_ExistingEmployee_ReturnsEmployeeDto()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var expectedEmployeeDto = new EmployeeDto
            {
                Id = employeeId,
                UserId = userId,
                TeamId = teamId,
                Role = "User",
                Status = "Active"
            };

            _fakeEmployeeRepository.Setup(x => x.GetEmployeeByTeamIdAndUserId(teamId, userId)).ReturnsAsync(expectedEmployeeDto);

            // Act
            var result = await _employeeService.GetEmployeeByTeamIdAndUserId(teamId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEmployeeDto.Id, result.Id);
            Assert.Equal(expectedEmployeeDto.UserId, result.UserId);
            Assert.Equal(expectedEmployeeDto.TeamId, result.TeamId);
            Assert.Equal(expectedEmployeeDto.Role, result.Role);
            Assert.Equal(expectedEmployeeDto.Status, result.Status);
        }
    }
}