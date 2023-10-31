using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PerformanceAssessmentApi.Controllers;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Controllers
{
    public class AssignSchedulerControllerTests
    {
        private readonly AssignSchedulerController _controller;
        private readonly Mock<IAssignSchedulerService> _fakeAssignSchedulerService;
        private readonly Mock<IAssessmentService> _fakeAssessmentService;
        private readonly Mock<IEmployeeAssignSchedulerNotificationService> _fakeEmployeeAssignSchedulerNotificationService;
        private readonly Mock<IAdminNotificationService> _fakeAdminNotificationService;
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly Mock<IUserService> _fakeUserService;
        private readonly Mock<ITeamService> _fakeTeamService;
        private readonly Mock<ILogger<AssignSchedulerController>> _logger;

        public AssignSchedulerControllerTests()
        {
            _fakeAssignSchedulerService = new Mock<IAssignSchedulerService>();
            _fakeAssessmentService = new Mock<IAssessmentService>();
            _fakeEmployeeAssignSchedulerNotificationService = new Mock<IEmployeeAssignSchedulerNotificationService>();
            _fakeAdminNotificationService = new Mock<IAdminNotificationService>();
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeUserService = new Mock<IUserService>();
            _fakeTeamService = new Mock<ITeamService>();
            _logger = new Mock<ILogger<AssignSchedulerController>>();
            _controller = new AssignSchedulerController(
                _fakeAssignSchedulerService.Object,
                _fakeAssessmentService.Object,
                _fakeEmployeeAssignSchedulerNotificationService.Object,
                _fakeAdminNotificationService.Object,
                _fakeEmployeeService.Object,
                _fakeUserService.Object,
                _fakeTeamService.Object,
                _logger.Object
            );
        }

        //[Fact]
        //public async Task CreateAssignSchedulers_ValidInput_ReturnsCreatedAtRouteResult()
        //{
        //    // Arrange
        //    var employeeIds = new List<int> { 1, 2, 3, 4, 5 };
        //    var assessmentId = It.IsAny<int>();
        //    var employeeId = It.IsAny<int>();
        //    var userId = It.IsAny<int>();
        //    var teamId = It.IsAny<int>();

        //    var assignSchedulerDto = new AssignSchedulerDetailsDto
        //    {
        //        EmployeeIds = employeeIds,
        //        Scheduler = new AssignSchedulerCreationDto
        //        {
        //            AssessmentId = assessmentId,
        //            Occurrence = "Daily",
        //            DueDate = "2023-10-16",
        //            Time = "14:30"
        //        }
        //    };

        //    var insertedIds = new List<int> { 1, 2, 3, 4, 5 };
        //    var assessment = new AssessmentDto();
        //    var employee = new EmployeeDto();
        //    var user = new UserDto();
        //    var team = new TeamDto();


        //    _fakeAssignSchedulerService.Setup(service => service.CreateAssignSchedulers(employeeIds, assignSchedulerDto.Scheduler))
        //        .ReturnsAsync(insertedIds);
        //    _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(insertedIds[0]))
        //        .ReturnsAsync(new AssignSchedulerDto());
        //    _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId)).ReturnsAsync(assessment);
        //    _fakeEmployeeService.Setup(service => service.GetEmployeeById(employeeId)).ReturnsAsync(employee);
        //    _fakeUserService.Setup(service => service.GetUserById(userId)).ReturnsAsync(user);
        //    _fakeTeamService.Setup(service => service.GetTeamById(teamId)).ReturnsAsync(team);

        //    // Act
        //    var result = await _controller.CreateAssignSchedulers(assignSchedulerDto);

        //    // Assert
        //    var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
        //    Assert.Equal("GetAssignSchedulerById", createdAtRouteResult.RouteName);
        //    Assert.Equal(insertedIds, createdAtRouteResult.RouteValues["id"]);
        //}

        [Fact]
        public async Task CreateAssignSchedulers_InvalidInput_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            var assignSchedulerDto = new AssignSchedulerDetailsDto
            {
                EmployeeIds = null!,
                Scheduler = new AssignSchedulerCreationDto
                {
                    AssessmentId = assessmentId,
                    Occurrence = "Daily",
                    DueDate = "2023-10-16",
                    Time = "14:30"
                }
            };

            // Act
            var result = await _controller.CreateAssignSchedulers(assignSchedulerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task CreateAssignSchedulers_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeIds = new List<int> { 1, 2, 3, 4, 5 };
            var assessmentId = It.IsAny<int>();

            var assignSchedulerDto = new AssignSchedulerDetailsDto
            {
                EmployeeIds = employeeIds,
                Scheduler = new AssignSchedulerCreationDto
                {
                    AssessmentId = assessmentId,
                    Occurrence = "Daily",
                    DueDate = "2023-10-16",
                    Time = "14:30"
                }
            };

            _fakeAssignSchedulerService.Setup(service => service.CreateAssignSchedulers(employeeIds, assignSchedulerDto.Scheduler))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateAssignSchedulers(assignSchedulerDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllAssignSchedulers_HasSchedulers_ReturnsOkObjectResult()
        {
            // Arrange
            var schedulers = new List<AssignSchedulerDto> { new AssignSchedulerDto() };

            _fakeAssignSchedulerService.Setup(service => service.GetAllAssignSchedulers())
                .ReturnsAsync(schedulers);

            // Act
            var result = await _controller.GetAllAssignSchedulers();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedSchedulers = Assert.IsType<List<AssignSchedulerDto>>(okObjectResult.Value);
            Assert.Equal(schedulers, returnedSchedulers);
        }

        [Fact]
        public async Task GetAllAssignSchedulers_HasNoSchedulers_ReturnsNoContentResult()
        {
            // Arrange
            _fakeAssignSchedulerService.Setup(service => service.GetAllAssignSchedulers())
                .ReturnsAsync(new List<AssignSchedulerDto>());

            // Act
            var result = await _controller.GetAllAssignSchedulers();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllAssignSchedulers_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeAssignSchedulerService.Setup(service => service.GetAllAssignSchedulers())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllAssignSchedulers();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAssignSchedulerById_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();

            var assignScheduler = new AssignSchedulerDto { Id = assignSchedulerId };

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .ReturnsAsync(assignScheduler);

            // Act
            var result = await _controller.GetAssignSchedulerById(assignSchedulerId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedScheduler = Assert.IsType<AssignSchedulerDto>(okObjectResult.Value);
            Assert.Equal(assignScheduler, returnedScheduler);
        }

        [Fact]
        public async Task GetAssignSchedulerById_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .ReturnsAsync((AssignSchedulerDto?)null!);

            // Act
            var result = await _controller.GetAssignSchedulerById(assignSchedulerId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssignSchedulerById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssignSchedulerById(assignSchedulerId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
        [Fact]
        public async Task GetAssignSchedulerByAssessmentId_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var assessmentId = 1;
            var assignSchedulers = new List<AssignSchedulerDto> { new AssignSchedulerDto() };

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByAssessmentId(assessmentId))
                .ReturnsAsync(assignSchedulers);

            // Act
            var result = await _controller.GetAssignSchedulerByAssessmentId(assessmentId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedSchedulers = Assert.IsType<List<AssignSchedulerDto>>(okObjectResult.Value);
            Assert.Equal(assignSchedulers, returnedSchedulers);
        }

        [Fact]
        public async Task GetAssignSchedulerByAssessmentId_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByAssessmentId(assessmentId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<AssignSchedulerDto>>(null!));

            // Act
            var result = await _controller.GetAssignSchedulerByAssessmentId(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssignSchedulerByAssessmentId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByAssessmentId(assessmentId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssignSchedulerByAssessmentId(assessmentId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAssignSchedulerByEmployeeId_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assignSchedulers = new List<AssignSchedulerDto> { new AssignSchedulerDto() };

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByEmployeeId(employeeId))
                .ReturnsAsync(assignSchedulers);

            // Act
            var result = await _controller.GetAssignSchedulerByEmployeeId(employeeId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedSchedulers = Assert.IsType<List<AssignSchedulerDto>>(okObjectResult.Value);
            Assert.Equal(assignSchedulers, returnedSchedulers);
        }

        [Fact]
        public async Task GetAssignSchedulerByEmployeeId_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByEmployeeId(employeeId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<AssignSchedulerDto>>(null!));

            // Act
            var result = await _controller.GetAssignSchedulerByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssignSchedulerByEmployeeId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByEmployeeId(employeeId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssignSchedulerByEmployeeId(employeeId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateAssignScheduler_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var updatedSchedulerDto = new AssignSchedulerUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                IsAnswered = true,
                Occurrence = "Weekly",
                DueDate = "2023-10-17",
                Time = "15:30"
            };

            var assignScheduler = new AssignSchedulerDto
            {
                Id = assignSchedulerId,
                AssessmentId = updatedSchedulerDto.AssessmentId,
                EmployeeId = updatedSchedulerDto.EmployeeId,
                IsAnswered = updatedSchedulerDto.IsAnswered,
                Occurrence = "Daily",
                DueDate = "2023-10-16",
                Time = "14:30"
            };

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .ReturnsAsync(assignScheduler);
            _fakeAssignSchedulerService.Setup(service => service.UpdateAssignScheduler(assignSchedulerId, updatedSchedulerDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateAssignScheduler(assignSchedulerId, updatedSchedulerDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAssignScheduler_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var updatedSchedulerDto = new AssignSchedulerUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                IsAnswered = true,
                Occurrence = "Weekly",
                DueDate = "2023-10-17",
                Time = "15:30"
            };

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .ReturnsAsync((AssignSchedulerDto?)null!);

            // Act
            var result = await _controller.UpdateAssignScheduler(assignSchedulerId, updatedSchedulerDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateAssignScheduler_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var updatedSchedulerDto = new AssignSchedulerUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                IsAnswered = true,
                Occurrence = "Weekly",
                DueDate = "2023-10-17",
                Time = "15:30"
            };

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateAssignScheduler(assignSchedulerId, updatedSchedulerDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        //[Fact]
        //public async Task DeleteAssignScheduler_ExistingAssignScheduler_ReturnsOkObjectResult()
        //{
        //    // Arrange
        //    var assignSchedulerId = It.IsAny<int>();

        //    _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
        //        .ReturnsAsync(new AssignSchedulerDto { Id = assignSchedulerId });
        //    _fakeAssignSchedulerService.Setup(service => service.DeleteAssignScheduler(assignSchedulerId))
        //        .ReturnsAsync(1);

        //    // Act
        //    var result = await _controller.DeleteAssignScheduler(assignSchedulerId);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task DeleteAssignScheduler_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .ReturnsAsync((AssignSchedulerDto?)null!);

            // Act
            var result = await _controller.DeleteAssignScheduler(assignSchedulerId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAssignScheduler_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assignSchedulerId = It.IsAny<int>();

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerById(assignSchedulerId))
                .ReturnsAsync(new AssignSchedulerDto { Id = assignSchedulerId });
            _fakeAssignSchedulerService.Setup(service => service.DeleteAssignScheduler(assignSchedulerId))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteAssignScheduler(assignSchedulerId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAssignSchedulerByEmployeeIdAndAssessmentId_ValidIds_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var assignScheduler = new AssignSchedulerDto();

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId))
                .ReturnsAsync(assignScheduler);

            // Act
            var result = await _controller.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedScheduler = Assert.IsType<AssignSchedulerDto>(okObjectResult.Value);
            Assert.Equal(assignScheduler, returnedScheduler);
        }

        [Fact]
        public async Task GetAssignSchedulerByEmployeeIdAndAssessmentId_InvalidIds_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId))
                .ReturnsAsync((AssignSchedulerDto)null!);

            // Act
            var result = await _controller.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssignSchedulerByEmployeeIdAndAssessmentId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            _fakeAssignSchedulerService.Setup(service => service.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
    }
}