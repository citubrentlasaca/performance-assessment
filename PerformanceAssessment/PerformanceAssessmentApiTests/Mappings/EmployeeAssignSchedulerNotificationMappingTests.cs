using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class EmployeeAssignSchedulerNotificationMappingTests
    {
        private readonly IMapper _mapper;

        public EmployeeAssignSchedulerNotificationMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EmployeeAssignSchedulerNotificationMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidEmployeeAssignSchedulerNotificationCreationDto_ReturnsEmployeeAssignSchedulerNotification()
        {
            // Arrange
            var employeeAssignSchedulerNotificationCreationDto = new EmployeeAssignSchedulerNotificationCreationDto
            {
                EmployeeId = 1,
                AssessmentId = 1,
            };

            // Act
            var notification = _mapper.Map<EmployeeAssignSchedulerNotification>(employeeAssignSchedulerNotificationCreationDto);

            // Assert
            Assert.Equal(employeeAssignSchedulerNotificationCreationDto.EmployeeId, notification.EmployeeId);
            Assert.Equal(employeeAssignSchedulerNotificationCreationDto.AssessmentId, notification.AssessmentId);
            Assert.NotNull(notification.DateTimeCreated);
        }
    }
}