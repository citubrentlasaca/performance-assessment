using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IEmployeeAssignSchedulerNotificationServiceTests
    {
        private readonly Mock<IEmployeeAssignSchedulerNotificationRepository> _fakeNotificationRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IEmployeeAssignSchedulerNotificationService _notificationService;

        public IEmployeeAssignSchedulerNotificationServiceTests()
        {
            _fakeNotificationRepository = new Mock<IEmployeeAssignSchedulerNotificationRepository>();
            _fakeMapper = new Mock<IMapper>();
            _notificationService = new EmployeeAssignSchedulerNotificationService(_fakeNotificationRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateEmployeeAssignSchedulerNotification_ValidDto_ReturnsCreatedNotification()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var notificationCreationDto = new EmployeeAssignSchedulerNotificationCreationDto
            {
                EmployeeId = employeeId,
                AssessmentId = assessmentId,
            };

            var notification = new EmployeeAssignSchedulerNotification
            {
                Id = notificationId,
                EmployeeId = employeeId,
                AssessmentId = assessmentId
            };

            _fakeMapper.Setup(x => x.Map<EmployeeAssignSchedulerNotification>(notificationCreationDto)).Returns(notification);
            _fakeNotificationRepository.Setup(x => x.CreateEmployeeAssignSchedulerNotification(notification)).ReturnsAsync(notification.Id);

            // Act
            var createdNotification = await _notificationService.CreateEmployeeAssignSchedulerNotification(notificationCreationDto);

            // Assert
            Assert.NotNull(createdNotification);
            Assert.Equal(notification.Id, createdNotification.Id);
            Assert.Equal(notification.EmployeeId, createdNotification.EmployeeId);
            Assert.Equal(notification.AssessmentId, createdNotification.AssessmentId);
        }

        [Fact]
        public async Task GetAllEmployeeAssignSchedulerNotifications_ExistingNotifications_ReturnsAllNotifications()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var notifications = new List<EmployeeAssignSchedulerNotificationDto>
            {
                new EmployeeAssignSchedulerNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AssessmentId = assessmentId
                },
                new EmployeeAssignSchedulerNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AssessmentId = assessmentId
                },
            };

            _fakeNotificationRepository.Setup(x => x.GetAllEmployeeAssignSchedulerNotifications()).ReturnsAsync(notifications);

            // Act
            var result = await _notificationService.GetAllEmployeeAssignSchedulerNotifications();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(notifications.Count, result.Count());
        }

        [Fact]
        public async Task GetEmployeeAssignSchedulerNotificationById_ExistingNotification_ReturnsNotificationDto()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var expectedNotificationDto = new EmployeeAssignSchedulerNotificationDto
            {
                Id = notificationId,
                EmployeeId = employeeId,
                AssessmentId = assessmentId
            };

            _fakeNotificationRepository.Setup(x => x.GetEmployeeAssignSchedulerNotificationById(notificationId)).ReturnsAsync(expectedNotificationDto);

            // Act
            var result = await _notificationService.GetEmployeeAssignSchedulerNotificationById(notificationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedNotificationDto.Id, result.Id);
            Assert.Equal(expectedNotificationDto.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedNotificationDto.AssessmentId, result.AssessmentId);
        }

        [Fact]
        public async Task GetAllEmployeeAssignSchedulerNotificationsByEmployeeId_ExistingNotifications_ReturnsNotificationDtos()
        {
            // Arrange
            var notificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var notifications = new List<EmployeeAssignSchedulerNotificationDto>
            {
                new EmployeeAssignSchedulerNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AssessmentId = assessmentId
                },
                new EmployeeAssignSchedulerNotificationDto
                {
                    Id = notificationId,
                    EmployeeId = employeeId,
                    AssessmentId = assessmentId
                },
            };

            _fakeNotificationRepository.Setup(x => x.GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId)).ReturnsAsync(notifications);

            // Act
            var result = await _notificationService.GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(notifications.Count, result.Count());
        }
    }
}