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
    public class EmployeeAnnouncementNotificationControllerTests
    {
        private readonly EmployeeAnnouncementNotificationController _controller;
        private readonly Mock<IEmployeeAnnouncementNotificationService> _fakeEmployeeNotificationService;
        private readonly Mock<ILogger<EmployeeAnnouncementNotificationController>> _logger;

        public EmployeeAnnouncementNotificationControllerTests()
        {
            _fakeEmployeeNotificationService = new Mock<IEmployeeAnnouncementNotificationService>();
            _logger = new Mock<ILogger<EmployeeAnnouncementNotificationController>>();
            _controller = new EmployeeAnnouncementNotificationController(_fakeEmployeeNotificationService.Object, _logger.Object);
        }

        [Fact]
        public async Task CreateEmployeeAnnouncementNotification_ValidInput_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var announcementId = It.IsAny<int>();

            var employeeNotification = new EmployeeAnnouncementNotificationCreationDto
            {
                EmployeeId = employeeId,
                AnnouncementId = announcementId
            };

            _fakeEmployeeNotificationService.Setup(service => service.CreateEmployeeAnnouncementNotification(employeeNotification))
                .ReturnsAsync(new EmployeeAnnouncementNotification());

            // Act
            var result = await _controller.CreateEmployeeAnnouncementNotification(employeeNotification);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateEmployeeAnnouncementNotification_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var announcementId = It.IsAny<int>();

            var employeeNotification = new EmployeeAnnouncementNotificationCreationDto
            {
                EmployeeId = employeeId,
                AnnouncementId = announcementId
            };

            _fakeEmployeeNotificationService.Setup(service => service.CreateEmployeeAnnouncementNotification(employeeNotification))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateEmployeeAnnouncementNotification(employeeNotification);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllEmployeeAnnouncementNotifications_ExistingNotifications_ReturnsOkObjectResult()
        {
            // Arrange
            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAnnouncementNotifications())
                .ReturnsAsync(new List<EmployeeAnnouncementNotificationDto> { new EmployeeAnnouncementNotificationDto() });

            // Act
            var result = await _controller.GetAllEmployeeAnnouncementNotifications();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllEmployeeAnnouncementNotifications_HasNoNotifications_ReturnsNoContentResult()
        {
            // Arrange
            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAnnouncementNotifications())
                .ReturnsAsync(await Task.FromResult<IEnumerable<EmployeeAnnouncementNotificationDto>>(null!));

            // Act
            var result = await _controller.GetAllEmployeeAnnouncementNotifications();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllEmployeeAnnouncementNotifications_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeEmployeeNotificationService.Setup(service => service.GetAllEmployeeAnnouncementNotifications())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllEmployeeAnnouncementNotifications();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationById_ExistingNotification_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAnnouncementNotificationById(id))
                .ReturnsAsync(new EmployeeAnnouncementNotificationDto());

            // Act
            var result = await _controller.GetEmployeeAnnouncementNotificationById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationById_MissingNotification_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAnnouncementNotificationById(id))
                .ReturnsAsync((EmployeeAnnouncementNotificationDto)null!);

            // Act
            var result = await _controller.GetEmployeeAnnouncementNotificationById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAnnouncementNotificationById(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeAnnouncementNotificationById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationByEmployeeId_ExistingNotification_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId))
                .ReturnsAsync(new List<EmployeeAnnouncementNotificationDto> { new EmployeeAnnouncementNotificationDto() });

            // Act
            var result = await _controller.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationByEmployeeId_MissingNotification_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<EmployeeAnnouncementNotificationDto>>(null!));

            // Act
            var result = await _controller.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationByEmployeeId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeEmployeeNotificationService.Setup(service => service.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}