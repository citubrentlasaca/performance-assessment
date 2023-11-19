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
    public class EmployeeAssignSchedulerNotificationControllerTests
    {
        private readonly EmployeeAssignSchedulerNotificationController _controller;
        private readonly Mock<IEmployeeAssignSchedulerNotificationService> _fakeEmployeeNotificationService;
        private readonly Mock<ILogger<EmployeeAssignSchedulerNotificationController>> _logger;

        public EmployeeAssignSchedulerNotificationControllerTests()
        {
            _fakeEmployeeNotificationService = new Mock<IEmployeeAssignSchedulerNotificationService>();
            _logger = new Mock<ILogger<EmployeeAssignSchedulerNotificationController>>();
            _controller = new EmployeeAssignSchedulerNotificationController(_fakeEmployeeNotificationService.Object, _logger.Object);
        }

        [Fact]
        public async Task CreateEmployeeAssignSchedulerNotification_ValidNotification_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var employeeNotificationCreationDto = new EmployeeAssignSchedulerNotificationCreationDto
            {
                EmployeeId = employeeId,
                AssessmentId = assessmentId
            };

            _fakeEmployeeNotificationService.Setup(service => service.CreateEmployeeAssignSchedulerNotification(employeeNotificationCreationDto))
                .ReturnsAsync(new EmployeeAssignSchedulerNotification());

            // Act
            var result = await _controller.CreateEmployeeAssignSchedulerNotification(employeeNotificationCreationDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateEmployeeAssignSchedulerNotification_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var employeeNotificationCreationDto = new EmployeeAssignSchedulerNotificationCreationDto
            {
                EmployeeId = employeeId,
                AssessmentId = assessmentId
            };

            _fakeEmployeeNotificationService.Setup(service => service.CreateEmployeeAssignSchedulerNotification(employeeNotificationCreationDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateEmployeeAssignSchedulerNotification(employeeNotificationCreationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllEmployeeAssignSchedulerNotifications_ExistingNotifications_ReturnsOkObjectResult()
        {
            // Arrange
            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAssignSchedulerNotifications())
                .ReturnsAsync(new List<EmployeeAssignSchedulerNotificationDto> { new EmployeeAssignSchedulerNotificationDto() });

            // Act
            var result = await _controller.GetAllEmployeeAssignSchedulerNotifications();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllEmployeeAssignSchedulerNotifications_HasNoNotifications_ReturnsNoContentResult()
        {
            // Arrange
            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAssignSchedulerNotifications())
                .ReturnsAsync(await Task.FromResult<IEnumerable<EmployeeAssignSchedulerNotificationDto>>(null!));

            // Act
            var result = await _controller.GetAllEmployeeAssignSchedulerNotifications();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllEmployeeAssignSchedulerNotifications_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAssignSchedulerNotifications())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllEmployeeAssignSchedulerNotifications();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAssignSchedulerNotificationById_ExistingNotification_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAssignSchedulerNotificationById(id))
                .ReturnsAsync(new EmployeeAssignSchedulerNotificationDto());

            // Act
            var result = await _controller.GetEmployeeAssignSchedulerNotificationById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeAssignSchedulerNotificationById_MissingNotification_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAssignSchedulerNotificationById(id))
                .ReturnsAsync((EmployeeAssignSchedulerNotificationDto?)null!);

            // Act
            var result = await _controller.GetEmployeeAssignSchedulerNotificationById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAssignSchedulerNotificationById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAssignSchedulerNotificationById(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeAssignSchedulerNotificationById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAssignSchedulerNotificationsByEmployeeId_ExistingNotifications_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId))
                .ReturnsAsync(new List<EmployeeAssignSchedulerNotificationDto> { new EmployeeAssignSchedulerNotificationDto() });

            // Act
            var result = await _controller.GetEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeAssignSchedulerNotificationsByEmployeeId_HasNoNotifications_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId))
                .ReturnsAsync((IEnumerable<EmployeeAssignSchedulerNotificationDto>)null!);

            // Act
            var result = await _controller.GetEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAssignSchedulerNotificationsByEmployeeId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}