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
    public class AnnouncementControllerTests
    {
        private readonly AnnouncementController _controller;
        private readonly Mock<IAnnouncementService> _fakeAnnouncementService;
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly Mock<IEmployeeAnnouncementNotificationService> _fakeEmployeeAnnouncementNotificationService;
        private readonly Mock<ILogger<AnnouncementController>> _logger;

        public AnnouncementControllerTests()
        {
            _fakeAnnouncementService = new Mock<IAnnouncementService>();
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeEmployeeAnnouncementNotificationService = new Mock<IEmployeeAnnouncementNotificationService>();
            _logger = new Mock<ILogger<AnnouncementController>>();
            _controller = new AnnouncementController(
                _fakeAnnouncementService.Object,
                _fakeEmployeeService.Object,
                _fakeEmployeeAnnouncementNotificationService.Object,
                _logger.Object
            );
        }

        [Fact]
        public async Task CreateAnnouncement_ValidAnnouncement_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            var announcement = new AnnouncementCreationDto
            {
                TeamId = teamId,
                Content = "Dear users, we will be performing scheduled maintenance on our system on October 17, 2023. During this time, the system will be temporarily unavailable. We apologize for any inconvenience this may cause and appreciate your understanding. Thank you for being a valued part of our community!"
            };

            _fakeAnnouncementService.Setup(service => service.CreateAnnouncement(announcement))
                .ReturnsAsync(new Announcement());

            _fakeEmployeeService.Setup(service => service.GetEmployeeByTeamId(announcement.TeamId))
                .ReturnsAsync(new List<EmployeeDto> { new EmployeeDto() });

            // Act
            var result = await _controller.CreateAnnouncement(announcement);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.IsType<Announcement>(createdAtRouteResult.Value);
        }

        [Fact]
        public async Task CreateAnnouncement_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            var announcement = new AnnouncementCreationDto
            {
                TeamId = teamId,
                Content = "Dear users, we will be performing scheduled maintenance on our system on October 17, 2023. During this time, the system will be temporarily unavailable. We apologize for any inconvenience this may cause and appreciate your understanding. Thank you for being a valued part of our community!"
            };

            _fakeAnnouncementService.Setup(service => service.CreateAnnouncement(announcement))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateAnnouncement(announcement);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllAnnouncements_HasAnnouncements_ReturnsOkObjectResult()
        {
            // Arrange
            _fakeAnnouncementService.Setup(service => service.GetAllAnnouncements())
                .ReturnsAsync(new List<AnnouncementDto> { new AnnouncementDto() });

            // Act
            var result = await _controller.GetAllAnnouncements();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllAnnouncements_HasNoAnnouncements_ReturnsNoContentResult()
        {
            // Arrange
            _fakeAnnouncementService.Setup(service => service.GetAllAnnouncements())
                .ReturnsAsync(new List<AnnouncementDto>());

            // Act
            var result = await _controller.GetAllAnnouncements();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllAnnouncements_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeAnnouncementService.Setup(service => service.GetAllAnnouncements())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllAnnouncements();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAnnouncementById_ExistingAnnouncement_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync(new AnnouncementDto());

            // Act
            var result = await _controller.GetAnnouncementById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAnnouncementById_MissingAnnouncement_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync((AnnouncementDto?)null!);

            // Act
            var result = await _controller.GetAnnouncementById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAnnouncementById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAnnouncementById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateAnnouncement_ExistingAnnouncement_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            var announcement = new AnnouncementUpdationDto
            {
                Content = "Dear team members, it's that time of the year again! We are approaching the end of the quarter, and it's time for our quarterly performance assessments. Your feedback and self-assessment are vital in helping us measure progress and set new goals. Please be prepared to discuss your achievements, challenges, and objectives with your respective managers. We look forward to a productive assessment process."
            };

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync(new AnnouncementDto());

            _fakeAnnouncementService.Setup(service => service.UpdateAnnouncement(id, announcement))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateAnnouncement(id, announcement);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAnnouncement_MissingAnnouncement_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            var announcement = new AnnouncementUpdationDto
            {
                Content = "Dear team members, it's that time of the year again! We are approaching the end of the quarter, and it's time for our quarterly performance assessments. Your feedback and self-assessment are vital in helping us measure progress and set new goals. Please be prepared to discuss your achievements, challenges, and objectives with your respective managers. We look forward to a productive assessment process."
            };

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync((AnnouncementDto?)null!);

            // Act
            var result = await _controller.UpdateAnnouncement(id, announcement);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateAnnouncement_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            var announcement = new AnnouncementUpdationDto
            {
                Content = "Dear team members, it's that time of the year again! We are approaching the end of the quarter, and it's time for our quarterly performance assessments. Your feedback and self-assessment are vital in helping us measure progress and set new goals. Please be prepared to discuss your achievements, challenges, and objectives with your respective managers. We look forward to a productive assessment process."
            };

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync(new AnnouncementDto());

            _fakeAnnouncementService.Setup(service => service.UpdateAnnouncement(id, announcement))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateAnnouncement(id, announcement);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAnnouncement_ExistingAnnouncement_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync(new AnnouncementDto());

            _fakeAnnouncementService.Setup(service => service.DeleteAnnouncement(id))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteAnnouncement(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAnnouncement_MissingAnnouncement_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync((AnnouncementDto?)null!);

            // Act
            var result = await _controller.DeleteAnnouncement(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAnnouncement_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnnouncementService.Setup(service => service.GetAnnouncementById(id))
                .ReturnsAsync(new AnnouncementDto());

            _fakeAnnouncementService.Setup(service => service.DeleteAnnouncement(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteAnnouncement(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}