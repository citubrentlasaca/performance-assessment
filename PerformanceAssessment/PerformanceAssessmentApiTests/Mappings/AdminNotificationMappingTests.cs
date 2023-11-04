using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class AdminNotificationMappingTests
    {
        private readonly IMapper _mapper;

        public AdminNotificationMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AdminNotificationMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidAdminNotificationCreationDto_ReturnsAdminNotification()
        {
            // Arrange
            var adminNotificationCreationDto = new AdminNotificationCreationDto
            {
                EmployeeId = 1,
                EmployeeName = "John Doe",
                AssessmentTitle = "Team Evaluation",
                TeamName = "Team WorkPA",
            };

            // Act
            var adminNotification = _mapper.Map<AdminNotification>(adminNotificationCreationDto);

            // Assert
            Assert.Equal(adminNotificationCreationDto.EmployeeId, adminNotification.EmployeeId);
            Assert.Equal(adminNotificationCreationDto.EmployeeName, adminNotification.EmployeeName);
            Assert.Equal(adminNotificationCreationDto.AssessmentTitle, adminNotification.AssessmentTitle);
            Assert.Equal(adminNotificationCreationDto.TeamName, adminNotification.TeamName);
            Assert.NotNull(adminNotification.DateTimeCreated);
        }
    }
}