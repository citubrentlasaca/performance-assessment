using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IAdminNotificationServiceTests
    {
        private readonly Mock<IAdminNotificationRepository> _fakeAdminNotificationRepository;
        private readonly Mock<IAssignSchedulerRepository> _fakeAssignSchedulerRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IAdminNotificationService _adminNotificationService;

        public IAdminNotificationServiceTests()
        {
            _fakeAdminNotificationRepository = new Mock<IAdminNotificationRepository>();
            _fakeAssignSchedulerRepository = new Mock<IAssignSchedulerRepository>();
            _fakeMapper = new Mock<IMapper>();
            _adminNotificationService = new AdminNotificationService(_fakeAdminNotificationRepository.Object, _fakeAssignSchedulerRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateAdminNotification_ValidData_ReturnsCreatedAdminNotification()
        {
            // Arrange
            var adminNotificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var adminNotificationCreationDto = new AdminNotificationCreationDto
            {
                EmployeeId = employeeId,
                EmployeeName = "John Doe",
                AssessmentTitle = "Performance Review",
                TeamName = "Team A"
            };

            var expectedAdminNotification = new AdminNotification
            {
                Id = adminNotificationId,
                EmployeeId = adminNotificationCreationDto.EmployeeId,
                EmployeeName = adminNotificationCreationDto.EmployeeName,
                AssessmentTitle = adminNotificationCreationDto.AssessmentTitle,
                TeamName = adminNotificationCreationDto.TeamName,
            };

            _fakeMapper.Setup(x => x.Map<AdminNotification>(adminNotificationCreationDto)).Returns(expectedAdminNotification);
            _fakeAdminNotificationRepository.Setup(x => x.CreateAdminNotification(expectedAdminNotification)).ReturnsAsync(adminNotificationId);

            // Act
            var result = await _adminNotificationService.CreateAdminNotification(adminNotificationCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(adminNotificationId, result.Id);
            Assert.Equal(adminNotificationCreationDto.EmployeeId, result.EmployeeId);
            Assert.Equal(adminNotificationCreationDto.EmployeeName, result.EmployeeName);
            Assert.Equal(adminNotificationCreationDto.AssessmentTitle, result.AssessmentTitle);
            Assert.Equal(adminNotificationCreationDto.TeamName, result.TeamName);
        }

        [Fact]
        public async Task GetAllAdminNotifications_ExistingData_ReturnsAdminNotifications()
        {
            // Arrange
            var adminNotificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAdminNotifications = new List<AdminNotificationDto>
            {
                new AdminNotificationDto
                {
                    Id = adminNotificationId,
                    EmployeeId = employeeId,
                    EmployeeName = "John Doe",
                    AssessmentTitle = "Performance Review",
                    TeamName = "Team A",
                    DateTimeCreated = "2023-10-28"
                },
                new AdminNotificationDto
                {
                    Id = adminNotificationId,
                    EmployeeId = employeeId,
                    EmployeeName = "Jane Smith",
                    AssessmentTitle = "Yearly Evaluation",
                    TeamName = "Team B",
                    DateTimeCreated = "2023-10-27"
                }
            };

            _fakeAdminNotificationRepository.Setup(x => x.GetAllAdminNotifications()).ReturnsAsync(expectedAdminNotifications);

            // Act
            var result = await _adminNotificationService.GetAllAdminNotifications();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAdminNotifications.Count, result.Count());
            var firstExpectedAdminNotification = expectedAdminNotifications.First();
            var firstResultAdminNotification = result.First();
            Assert.Equal(firstExpectedAdminNotification.Id, firstResultAdminNotification.Id);
            Assert.Equal(firstExpectedAdminNotification.EmployeeId, firstResultAdminNotification.EmployeeId);
            Assert.Equal(firstExpectedAdminNotification.EmployeeName, firstResultAdminNotification.EmployeeName);
            Assert.Equal(firstExpectedAdminNotification.AssessmentTitle, firstResultAdminNotification.AssessmentTitle);
            Assert.Equal(firstExpectedAdminNotification.TeamName, firstResultAdminNotification.TeamName);
            Assert.Equal(firstExpectedAdminNotification.DateTimeCreated, firstResultAdminNotification.DateTimeCreated);
        }

        [Fact]
        public async Task GetAdminNotificationById_ExistingAdminNotification_ReturnsAdminNotification()
        {
            // Arrange
            var adminNotificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAdminNotification = new AdminNotificationDto
            {
                Id = adminNotificationId,
                EmployeeId = employeeId,
                EmployeeName = "John Doe",
                AssessmentTitle = "Performance Review",
                TeamName = "Team A",
                DateTimeCreated = "2023-10-28"
            };

            _fakeAdminNotificationRepository.Setup(x => x.GetAdminNotificationById(adminNotificationId)).ReturnsAsync(expectedAdminNotification);

            // Act
            var result = await _adminNotificationService.GetAdminNotificationById(adminNotificationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAdminNotification.Id, result.Id);
            Assert.Equal(expectedAdminNotification.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedAdminNotification.EmployeeName, result.EmployeeName);
            Assert.Equal(expectedAdminNotification.AssessmentTitle, result.AssessmentTitle);
            Assert.Equal(expectedAdminNotification.TeamName, result.TeamName);
            Assert.Equal(expectedAdminNotification.DateTimeCreated, result.DateTimeCreated);
        }

        [Fact]
        public async Task GetAdminNotificationByEmployeeId_ExistingData_ReturnsAdminNotifications()
        {
            // Arrange
            var adminNotificationId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAdminNotifications = new List<AdminNotificationDto>
            {
                new AdminNotificationDto
                {
                    Id = adminNotificationId,
                    EmployeeId = employeeId,
                    EmployeeName = "John Doe",
                    AssessmentTitle = "Performance Review",
                    TeamName = "Team A",
                    DateTimeCreated = "2023-10-28"
                },
                new AdminNotificationDto
                {
                    Id = adminNotificationId,
                    EmployeeId = employeeId,
                    EmployeeName = "Jane Smith",
                    AssessmentTitle = "Yearly Evaluation",
                    TeamName = "Team B",
                    DateTimeCreated = "2023-10-27"
                }
            };

            _fakeAdminNotificationRepository.Setup(x => x.GetAdminNotificationByEmployeeId(employeeId)).ReturnsAsync(expectedAdminNotifications);

            // Act
            var result = await _adminNotificationService.GetAdminNotificationByEmployeeId(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAdminNotifications.Count, result.Count());
            var firstExpectedAdminNotification = expectedAdminNotifications.First();
            var firstResultAdminNotification = result.First();
            Assert.Equal(firstExpectedAdminNotification.Id, firstResultAdminNotification.Id);
            Assert.Equal(firstExpectedAdminNotification.EmployeeId, firstResultAdminNotification.EmployeeId);
            Assert.Equal(firstExpectedAdminNotification.EmployeeName, firstResultAdminNotification.EmployeeName);
            Assert.Equal(firstExpectedAdminNotification.AssessmentTitle, firstResultAdminNotification.AssessmentTitle);
            Assert.Equal(firstExpectedAdminNotification.TeamName, firstResultAdminNotification.TeamName);
            Assert.Equal(firstExpectedAdminNotification.DateTimeCreated, firstResultAdminNotification.DateTimeCreated);
        }

        [Fact]
        public async Task ExecuteAdminNotificationAsync_HasNotAnswered_ReturnsCreatedAdminNotification()
        {
            // Arrange
            var adminNotificationId = It.IsAny<int>();
            var schedulerId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var adminNotificationCreationDto = new AdminNotificationCreationDto
            {
                EmployeeId = employeeId,
                EmployeeName = "John Doe",
                AssessmentTitle = "Performance Review",
                TeamName = "Team A"
            };

            var currentScheduler = new AssignSchedulerDto
            {
                Id = schedulerId,
                IsAnswered = false
            };

            var expectedAdminNotification = new AdminNotification
            {
                Id = adminNotificationId,
                EmployeeId = adminNotificationCreationDto.EmployeeId,
                EmployeeName = adminNotificationCreationDto.EmployeeName,
                AssessmentTitle = adminNotificationCreationDto.AssessmentTitle,
                TeamName = adminNotificationCreationDto.TeamName,
            };

            _fakeMapper.Setup(x => x.Map<AdminNotification>(adminNotificationCreationDto)).Returns(expectedAdminNotification);
            _fakeAssignSchedulerRepository.Setup(x => x.GetAssignSchedulerById(schedulerId)).ReturnsAsync(currentScheduler);
            _fakeAdminNotificationRepository.Setup(x => x.CreateAdminNotification(expectedAdminNotification)).ReturnsAsync(adminNotificationId);

            // Act
            await _adminNotificationService.ExecuteAdminNotificationAsync(schedulerId, adminNotificationCreationDto);

            // Assert
            _fakeAdminNotificationRepository.Verify(x => x.CreateAdminNotification(expectedAdminNotification), Times.Once);
        }
    }
}