using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class AssignSchedulerMappingTests
    {
        private readonly IMapper _mapper;

        public AssignSchedulerMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AssignSchedulerMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidAssignSchedulerCreationDto_ReturnsAssignScheduler()
        {
            // Arrange
            var assignSchedulerCreationDto = new AssignSchedulerCreationDto
            {
                AssessmentId = 1,
                Occurrence = "Daily",
                DueDate = "2023-10-31 14:30:00",
                Time = "14:30",
            };

            // Act
            var assignScheduler = _mapper.Map<AssignScheduler>(assignSchedulerCreationDto);

            // Assert
            Assert.Equal(assignSchedulerCreationDto.AssessmentId, assignScheduler.AssessmentId);
            Assert.Equal(assignSchedulerCreationDto.Occurrence, assignScheduler.Occurrence);
            Assert.Equal(assignSchedulerCreationDto.DueDate, assignScheduler.DueDate);
            Assert.Equal(assignSchedulerCreationDto.Time, assignScheduler.Time);
            Assert.NotNull(assignScheduler.DateTimeCreated);
            Assert.NotNull(assignScheduler.DateTimeUpdated);
        }

        [Fact]
        public void Map_ValidAssignSchedulerUpdationDto_ReturnsAssignScheduler()
        {
            // Arrange
            var assignSchedulerUpdationDto = new AssignSchedulerUpdationDto
            {
                IsAnswered = true,
                Occurrence = "Once",
                DueDate = "2023-11-30 09:45:00",
                Time = "09:45",
            };

            // Act
            var assignScheduler = _mapper.Map<AssignScheduler>(assignSchedulerUpdationDto);

            // Assert
            Assert.Equal(assignSchedulerUpdationDto.IsAnswered, assignScheduler.IsAnswered);
            Assert.Equal(assignSchedulerUpdationDto.Occurrence, assignScheduler.Occurrence);
            Assert.Equal(assignSchedulerUpdationDto.DueDate, assignScheduler.DueDate);
            Assert.Equal(assignSchedulerUpdationDto.Time, assignScheduler.Time);
            Assert.NotNull(assignScheduler.DateTimeUpdated);
        }

        [Fact]
        public void Map_ValidAssignSchedulerDto_ReturnsAssignSchedulerUpdationDto()
        {
            // Arrange
            var assignSchedulerDto = new AssignSchedulerDto
            {
                Id = 1,
                AssessmentId = 2,
                EmployeeId = 3,
                IsAnswered = false,
                Occurrence = "Twice",
                DueDate = "2023-12-15 08:15:00",
                Time = "08:15",
                DateTimeCreated = "2023-10-25 12:00:00",
                DateTimeUpdated = "2023-10-25 12:00:00",
            };

            // Act
            var assignSchedulerUpdationDto = _mapper.Map<AssignSchedulerUpdationDto>(assignSchedulerDto);

            // Assert
            Assert.Equal(assignSchedulerDto.AssessmentId, assignSchedulerUpdationDto.AssessmentId);
            Assert.Equal(assignSchedulerDto.EmployeeId, assignSchedulerUpdationDto.EmployeeId);
            Assert.Equal(assignSchedulerDto.IsAnswered, assignSchedulerUpdationDto.IsAnswered);
            Assert.Equal(assignSchedulerDto.Occurrence, assignSchedulerUpdationDto.Occurrence);
            Assert.Equal(assignSchedulerDto.DueDate, assignSchedulerUpdationDto.DueDate);
            Assert.Equal(assignSchedulerDto.Time, assignSchedulerUpdationDto.Time);
        }
    }
}