using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class EmployeeAnnouncementNotificationMappingTests
    {
        private readonly IMapper _mapper;

        public EmployeeAnnouncementNotificationMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EmployeeAnnouncementNotificationMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidEmployeeAnnouncementNotificationCreationDto_ReturnsEmployeeAnnouncementNotification()
        {
            // Arrange
            var employeeAnnouncementNotificationCreationDto = new EmployeeAnnouncementNotificationCreationDto
            {
                EmployeeId = 1,
                AnnouncementId = 1,
            };

            // Act
            var notification = _mapper.Map<EmployeeAnnouncementNotification>(employeeAnnouncementNotificationCreationDto);

            // Assert
            Assert.Equal(employeeAnnouncementNotificationCreationDto.EmployeeId, notification.EmployeeId);
            Assert.Equal(employeeAnnouncementNotificationCreationDto.AnnouncementId, notification.AnnouncementId);
            Assert.NotNull(notification.DateTimeCreated);
        }
    }
}