using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IEmployeeAnnouncementNotificationServiceTests
    {
        private readonly Mock<IEmployeeAnnouncementNotificationRepository> _fakeNotificationRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IEmployeeAnnouncementNotificationService _notificationService;

        public IEmployeeAnnouncementNotificationServiceTests()
        {
            _fakeNotificationRepository = new Mock<IEmployeeAnnouncementNotificationRepository>();
            _fakeMapper = new Mock<IMapper>();
            _notificationService = new EmployeeAnnouncementNotificationService(_fakeNotificationRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateEmployeeAnnouncementNotification_ValidDto_ReturnsCreatedNotification()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var announcementId = It.IsAny<int>();

            var notificationCreationDto = new EmployeeAnnouncementNotificationCreationDto
            {
                EmployeeId = employeeId,
                AnnouncementId = announcementId
            };

            var notification = new EmployeeAnnouncementNotification
            {
                Id = notificationId,
                EmployeeId = employeeId,
                AnnouncementId = announcementId
            };

            _fakeMapper.Setup(x => x.Map<EmployeeAnnouncementNotification>(notificationCreationDto)).Returns(notification);
            _fakeNotificationRepository.Setup(x => x.CreateEmployeeAnnouncementNotification(notification)).ReturnsAsync(notification.Id);

            // Act
            var createdNotification = await _notificationService.CreateEmployeeAnnouncementNotification(notificationCreationDto);

            // Assert
            Assert.NotNull(createdNotification);
            Assert.Equal(notification.Id, createdNotification.Id);
            Assert.Equal(notification.EmployeeId, createdNotification.EmployeeId);
            Assert.Equal(notification.AnnouncementId, createdNotification.AnnouncementId);
        }

        [Fact]
        public async Task GetAllEmployeeAnnouncementNotifications_ExistingNotifications_ReturnsAllNotifications()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var announcementId = It.IsAny<int>();

            var notifications = new List<EmployeeAnnouncementNotificationDto>
            {
                new EmployeeAnnouncementNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AnnouncementId = announcementId
                },
                new EmployeeAnnouncementNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AnnouncementId = announcementId
                },
            };

            _fakeNotificationRepository.Setup(x => x.GetAllEmployeeAnnouncementNotifications()).ReturnsAsync(notifications);

            // Act
            var result = await _notificationService.GetAllEmployeeAnnouncementNotifications();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(notifications.Count, result.Count());
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationById_ExistingNotification_ReturnsNotificationDto()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var announcementId = It.IsAny<int>();

            var expectedNotificationDto = new EmployeeAnnouncementNotificationDto
            {
                Id = notificationId,
                EmployeeId = employeeId,
                AnnouncementId = announcementId
            };

            _fakeNotificationRepository.Setup(x => x.GetEmployeeAnnouncementNotificationById(notificationId)).ReturnsAsync(expectedNotificationDto);

            // Act
            var result = await _notificationService.GetEmployeeAnnouncementNotificationById(notificationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedNotificationDto.Id, result.Id);
            Assert.Equal(expectedNotificationDto.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedNotificationDto.AnnouncementId, result.AnnouncementId);
        }

        [Fact]
        public async Task GetEmployeeAnnouncementNotificationByEmployeeId_ExistingNotifications_ReturnsNotificationDtos()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var announcementId = It.IsAny<int>();

            var notifications = new List<EmployeeAnnouncementNotificationDto>
            {
                new EmployeeAnnouncementNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AnnouncementId = announcementId
                },
                new EmployeeAnnouncementNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AnnouncementId = announcementId
                },
            };

            _fakeNotificationRepository.Setup(x => x.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId)).ReturnsAsync(notifications);

            // Act
            var result = await _notificationService.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(notifications.Count, result.Count());
        }
    }
}