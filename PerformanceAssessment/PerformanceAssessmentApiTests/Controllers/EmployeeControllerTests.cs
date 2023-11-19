using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PerformanceAssessmentApi.Controllers;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly EmployeeController _controller;
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly Mock<ITeamService> _fakeTeamService;
        private readonly Mock<ILogger<EmployeeController>> _fakeLogger;

        public EmployeeControllerTests()
        {
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeTeamService = new Mock<ITeamService>();
            _fakeLogger = new Mock<ILogger<EmployeeController>>();
            _controller = new EmployeeController(_fakeEmployeeService.Object, _fakeTeamService.Object, _fakeLogger.Object);
        }

        [Fact]
        public async Task CreateEmployee_ValidEmployee_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeDto = new EmployeeCreationDto
            {
                UserId = userId,
                TeamId = teamId
            };

            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamId(employeeDto.UserId, employeeDto.TeamId))
                .ReturnsAsync((Employee)null!);
            _fakeEmployeeService.Setup(service => service.CreateEmployee(employeeDto))
                .ReturnsAsync(new Employee());

            // Act
            var result = await _controller.CreateEmployee(employeeDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateEmployee_DuplicateEmployee_ReturnsConflictResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeDto = new EmployeeCreationDto
            {
                UserId = userId,
                TeamId = teamId
            };

            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamId(employeeDto.UserId, employeeDto.TeamId))
                .ReturnsAsync(new Employee());

            // Act
            var result = await _controller.CreateEmployee(employeeDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status409Conflict, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateEmployee_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeDto = new EmployeeCreationDto
            {
                UserId = userId,
                TeamId = teamId
            };

            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamId(employeeDto.UserId, employeeDto.TeamId))
                .ReturnsAsync((Employee)null!);
            _fakeEmployeeService.Setup(service => service.CreateEmployee(employeeDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateEmployee(employeeDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateEmployeeWithTeamCode_ValidEmployee_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var employeeDto = new EmployeeTeamInfoDto
            {
                UserId = userId,
                TeamCode = Guid.NewGuid()
            };

            _fakeTeamService.Setup(service => service.GetTeamByCode(employeeDto.TeamCode))
                .ReturnsAsync(new TeamDto());
            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamCode(employeeDto.UserId, employeeDto.TeamCode))
                .ReturnsAsync((EmployeeDto)null!);
            _fakeEmployeeService.Setup(service => service.CreateEmployeeWithTeamCode(employeeDto))
                .ReturnsAsync(new Employee());

            // Act
            var result = await _controller.CreateEmployeeWithTeamCode(employeeDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateEmployeeWithTeamCode_InvalidTeam_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var employeeDto = new EmployeeTeamInfoDto
            {
                UserId = userId,
                TeamCode = Guid.NewGuid()
            };

            _fakeTeamService.Setup(service => service.GetTeamByCode(employeeDto.TeamCode))
                .ReturnsAsync((TeamDto)null!);

            // Act
            var result = await _controller.CreateEmployeeWithTeamCode(employeeDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateEmployeeWithTeamCode_DuplicateEmployee_ReturnsConflictResult()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var employeeDto = new EmployeeTeamInfoDto
            {
                UserId = userId,
                TeamCode = Guid.NewGuid()
            };

            _fakeTeamService.Setup(service => service.GetTeamByCode(employeeDto.TeamCode))
                .ReturnsAsync(new TeamDto());
            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamCode(employeeDto.UserId, employeeDto.TeamCode))
                .ReturnsAsync(new EmployeeDto());

            // Act
            var result = await _controller.CreateEmployeeWithTeamCode(employeeDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status409Conflict, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateEmployeeWithTeamCode_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var employeeDto = new EmployeeTeamInfoDto
            {
                UserId = userId,
                TeamCode = Guid.NewGuid()
            };

            _fakeTeamService.Setup(service => service.GetTeamByCode(employeeDto.TeamCode))
                .ReturnsAsync(new TeamDto());
            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamCode(employeeDto.UserId, employeeDto.TeamCode))
                .ReturnsAsync((EmployeeDto)null!);
            _fakeEmployeeService.Setup(service => service.CreateEmployeeWithTeamCode(employeeDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateEmployeeWithTeamCode(employeeDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllEmployees_ExistingEmployees_ReturnsOkObjectResult()
        {
            // Arrange
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto(),
                new EmployeeDto()
            };

            _fakeEmployeeService.Setup(service => service.GetAllEmployees())
                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllEmployees_HasNoEmployees_ReturnsNoContentResult()
        {
            // Arrange
            _fakeEmployeeService.Setup(service => service.GetAllEmployees())
                .ReturnsAsync(new List<EmployeeDto>());

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllEmployees_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeEmployeeService.Setup(service => service.GetAllEmployees())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeById_ExistingEmployee_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(employeeId))
                .ReturnsAsync(new EmployeeDto { Id = employeeId });

            // Act
            var result = await _controller.GetEmployeeById(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetEmployeeById_MissingEmployee_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(employeeId))
                .ReturnsAsync((EmployeeDto)null!);

            // Act
            var result = await _controller.GetEmployeeById(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(employeeId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeById(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByUserId_ExistingEmployee_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserId(userId))
                .ReturnsAsync(new List<EmployeeDto>());

            // Act
            var result = await _controller.GetEmployeeByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByUserId_MissingEmployee_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserId(userId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<EmployeeDto>>(null!));

            // Act
            var result = await _controller.GetEmployeeByUserId(userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByUserId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserId(userId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeByUserId(userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByTeamId_ExistingTeamId_ReturnsOkObjectResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var employees = new List<EmployeeDto> { new EmployeeDto { TeamId = teamId } };

            _fakeEmployeeService.Setup(service => service.GetEmployeeByTeamId(teamId))
                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetEmployeeByTeamId(teamId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByTeamId_MissingEmployee_ReturnsNotFoundResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByTeamId(teamId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<EmployeeDto>>(null!));

            // Act
            var result = await _controller.GetEmployeeByTeamId(teamId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByTeamId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByTeamId(teamId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeByTeamId(teamId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateEmployee_ExistingEmployee_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeDto = new EmployeeUpdationDto
            {
                UserId = userId,
                TeamId = teamId,
                Status = "Inactive",
                Role = "Admin"
            };

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(id))
                .ReturnsAsync(new EmployeeDto { Id = id });
            _fakeEmployeeService.Setup(service => service.UpdateEmployee(id, employeeDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateEmployee(id, employeeDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEmployee_MissingEmployee_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeDto = new EmployeeUpdationDto
            {
                UserId = userId,
                TeamId = teamId,
                Status = "Inactive",
                Role = "Admin"
            };

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(id))
                .ReturnsAsync((EmployeeDto)null!);

            // Act
            var result = await _controller.UpdateEmployee(id, employeeDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateEmployee_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var employeeDto = new EmployeeUpdationDto
            {
                UserId = userId,
                TeamId = teamId,
                Status = "Inactive",
                Role = "Admin"
            };

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(id))
                .ReturnsAsync(new EmployeeDto());
            _fakeEmployeeService.Setup(service => service.UpdateEmployee(id, employeeDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateEmployee(id, employeeDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteEmployee_ExistingEmployee_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(id))
                .ReturnsAsync(new EmployeeDto());
            _fakeEmployeeService.Setup(service => service.DeleteEmployee(id))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteEmployee(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteEmployee_MissingEmployee_ReturnsNotFoundResult()
        {
            var id = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(id))
                .ReturnsAsync((EmployeeDto?)null!);

            // Act
            var result = await _controller.DeleteEmployee(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteEmployee_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeById(id))
                .ReturnsAsync(new EmployeeDto());
            _fakeEmployeeService.Setup(service => service.DeleteEmployee(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteEmployee(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeDetailsById_ExistingEmployee_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeDetailsById(employeeId))
                .ReturnsAsync(new EmployeeDetailsDto { Id = employeeId });

            // Act
            var result = await _controller.GetEmployeeDetailsById(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetEmployeeDetailsById_MissingEmployee_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeDetailsById(employeeId))
                .ReturnsAsync((EmployeeDetailsDto)null!);

            // Act
            var result = await _controller.GetEmployeeDetailsById(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeDetailsById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeDetailsById(employeeId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeDetailsById(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByTeamIdAndUserId_ExistingEmployee_ReturnsOkObjectResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var expectedEmployee = new EmployeeDto();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByTeamIdAndUserId(teamId, userId))
                .ReturnsAsync(expectedEmployee);

            // Act
            var result = await _controller.GetEmployeeByTeamIdAndUserId(teamId, userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualEmployee = Assert.IsType<EmployeeDto>(okResult.Value);
            Assert.Equal(expectedEmployee, actualEmployee);
        }

        [Fact]
        public async Task GetEmployeeByTeamIdAndUserId_MissingEmployee_ReturnsNotFoundResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var userId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByTeamIdAndUserId(teamId, userId))
                .ReturnsAsync((EmployeeDto)null!);

            // Act
            var result = await _controller.GetEmployeeByTeamIdAndUserId(teamId, userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeByTeamIdAndUserId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var userId = It.IsAny<int>();

            _fakeEmployeeService.Setup(service => service.GetEmployeeByTeamIdAndUserId(teamId, userId))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetEmployeeByTeamIdAndUserId(teamId, userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}