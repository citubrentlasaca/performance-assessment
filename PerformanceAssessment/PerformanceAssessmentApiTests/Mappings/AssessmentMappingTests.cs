using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class AssessmentMappingTests
    {
        private readonly IMapper _mapper;

        public AssessmentMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AssessmentMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidAssessmentCreationDto_ReturnsAssessment()
        {
            // Arrange
            var assessmentCreationDto = new AssessmentCreationDto
            {
                EmployeeId = 1,
                Title = "Daily Performance Report",
                Description = "Analysis of daily work performance",
            };

            // Act
            var assessment = _mapper.Map<Assessment>(assessmentCreationDto);

            // Assert
            Assert.Equal(assessmentCreationDto.EmployeeId, assessment.EmployeeId);
            Assert.Equal(assessmentCreationDto.Title, assessment.Title);
            Assert.Equal(assessmentCreationDto.Description, assessment.Description);
            Assert.NotNull(assessment.DateTimeCreated);
            Assert.NotNull(assessment.DateTimeUpdated);
        }

        [Fact]
        public void Map_ValidAssessmentUpdationDto_ReturnsAssessment()
        {
            // Arrange
            var assessmentUpdationDto = new AssessmentUpdationDto
            {
                Title = "Quarterly Performance Review",
                Description = "Assessment progress report for Q3 2023"
            };

            // Act
            var assessment = _mapper.Map<Assessment>(assessmentUpdationDto);

            // Assert
            Assert.Equal(assessmentUpdationDto.Title, assessment.Title);
            Assert.Equal(assessmentUpdationDto.Description, assessment.Description);
            Assert.NotNull(assessment.DateTimeUpdated);
        }
    }
}