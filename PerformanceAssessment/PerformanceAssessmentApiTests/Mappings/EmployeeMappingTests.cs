using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class EmployeeMappingTests
    {
        private readonly IMapper _mapper;

        public EmployeeMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EmployeeMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidEmployeeCreationDto_ReturnsEmployee()
        {
            // Arrange
            var employeeCreationDto = new EmployeeCreationDto
            {
                UserId = 1,
                TeamId = 1,
            };

            // Act
            var employee = _mapper.Map<Employee>(employeeCreationDto);

            // Assert
            Assert.Equal(employeeCreationDto.UserId, employee.UserId);
            Assert.Equal(employeeCreationDto.TeamId, employee.TeamId);
            Assert.NotNull(employee.DateTimeJoined);
        }

        [Fact]
        public void Map_ValidEmployeeTeamInfoDto_ReturnsEmployee()
        {
            // Arrange
            var employeeTeamInfoDto = new EmployeeTeamInfoDto
            {
                UserId = 2,
                TeamCode = Guid.NewGuid(),
            };

            // Act
            var employee = _mapper.Map<Employee>(employeeTeamInfoDto);

            // Assert
            Assert.Equal(employeeTeamInfoDto.UserId, employee.UserId);
            Assert.Equal("User", employee.Role);
            Assert.Equal("Active", employee.Status);
            Assert.NotNull(employee.DateTimeJoined);
        }

        [Fact]
        public void Map_ValidEmployeeUpdationDto_ReturnsEmployee()
        {
            // Arrange
            var employeeUpdationDto = new EmployeeUpdationDto
            {
                UserId = 3,
                TeamId = 3,
                Status = "Inactive",
                Role = "Admin",
            };

            // Act
            var employee = _mapper.Map<Employee>(employeeUpdationDto);

            // Assert
            Assert.Equal(employeeUpdationDto.UserId, employee.UserId);
            Assert.Equal(employeeUpdationDto.TeamId, employee.TeamId);
            Assert.Equal(employeeUpdationDto.Status, employee.Status);
            Assert.Equal(employeeUpdationDto.Role, employee.Role);
        }
    }
}