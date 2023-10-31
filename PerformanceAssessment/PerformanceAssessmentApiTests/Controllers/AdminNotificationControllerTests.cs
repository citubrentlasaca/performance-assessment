using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PerformanceAssessmentApi.Controllers;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Services;


namespace PerformanceAssessmentApiTests.Controllers
{
    public class AdminNotificationControllerTests
    {
        private readonly AdminNotificationController _controller;
        private readonly Mock<IAdminNotificationService> _fakeAdminNotificationService;
        private readonly Mock<ILogger<AdminNotificationController>> _logger;

        public AdminNotificationControllerTests()
        {
            _fakeAdminNotificationService = new Mock<IAdminNotificationService>();
            _logger = new Mock<ILogger<AdminNotificationController>>();

            _controller = new AdminNotificationController(_fakeAdminNotificationService.Object, _logger.Object);
        }

        [Fact]
        public async Task CreateAdminNotification_ValidAdminNotification_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            var adminNotification = new AdminNotificationCreationDto
            {
                EmployeeId = employeeId,
                EmployeeName = "John Doe",
                AssessmentTitle = "Team Evaluation",
                TeamName = "Team WorkPA"
            };
            _fakeAdminNotificationService.Setup(service => service.CreateAdminNotification(adminNotification))
                .ReturnsAsync(new AdminNotification());

            // Act
            var result = await _controller.CreateAdminNotification(adminNotification);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateAdminNotification_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            var adminNotification = new AdminNotificationCreationDto
            {
                EmployeeId = employeeId,
                EmployeeName = "John Doe",
                AssessmentTitle = "Team Evaluation",
                TeamName = "Team WorkPA"
            };
            _fakeAdminNotificationService.Setup(service => service.CreateAdminNotification(adminNotification))
                .Throws(new Exception());

            // Act 
            var result = await _controller.CreateAdminNotification(adminNotification);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllAdminNotifications_HasAdminNotifications_ReturnsOkObjectResult()
        {
            // Arrange
            _fakeAdminNotificationService.Setup(service => service.GetAllAdminNotifications())
                .ReturnsAsync(new List<AdminNotificationDto> { new AdminNotificationDto() });

            // Act 
            var result = await _controller.GetAllAdminNotifications();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllAdminNotifications_HasNoAdminNotifications_ReturnsNoContentResult()
        {
            // Arrange
            _fakeAdminNotificationService.Setup(service => service.GetAllAdminNotifications())
                .ReturnsAsync(new List<AdminNotificationDto>());

            // Act 
            var result = await _controller.GetAllAdminNotifications();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllAdminNotifications_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeAdminNotificationService.Setup(service => service.GetAllAdminNotifications())
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetAllAdminNotifications();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAdminNotificationById_ExistingAdminNotification_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAdminNotificationService.Setup(service => service.GetAdminNotificationById(id))
                .ReturnsAsync(new AdminNotificationDto());

            // Act 
            var result = await _controller.GetAdminNotificationById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAdminNotificationById_MissingAdminNotification_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAdminNotificationService.Setup(service => service.GetAdminNotificationById(id))
                .ReturnsAsync(await Task.FromResult<AdminNotificationDto>(null!));

            // Act 
            var result = await _controller.GetAdminNotificationById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAdminNotificationById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAdminNotificationService.Setup(service => service.GetAdminNotificationById(id))
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetAdminNotificationById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAdminNotificationByEmployeeId_ExistingAdminNotification_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeAdminNotificationService.Setup(service => service.GetAdminNotificationByEmployeeId(employeeId))
                .ReturnsAsync(new List<AdminNotificationDto>());

            // Act 
            var result = await _controller.GetAdminNotificationByEmployeeId(employeeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAdminNotificationByEmployeeId_MissingAdminNotification_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeAdminNotificationService.Setup(service => service.GetAdminNotificationByEmployeeId(employeeId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<AdminNotificationDto>>(null!));

            // Act 
            var result = await _controller.GetAdminNotificationByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAdminNotificationByEmployeeId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeAdminNotificationService.Setup(service => service.GetAdminNotificationByEmployeeId(employeeId))
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetAdminNotificationByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}