using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class ResultMappingTests
    {
        private readonly IMapper _mapper;

        public ResultMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new ResultMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidResultCreationDto_ReturnsResult()
        {
            // Arrange
            var resultCreationDto = new ResultCreationDto
            {
                AssessmentId = 1,
                EmployeeId = 1,
                Score = 50,
                DateTimeDue = "2023-10-31 15:45:00",
            };

            // Act
            var result = _mapper.Map<Result>(resultCreationDto);

            // Assert
            Assert.Equal(resultCreationDto.AssessmentId, result.AssessmentId);
            Assert.Equal(resultCreationDto.EmployeeId, result.EmployeeId);
            Assert.Equal(resultCreationDto.Score, result.Score);
            Assert.Equal(resultCreationDto.DateTimeDue, result.DateTimeDue);
            Assert.NotNull(result.DateTimeCreated);
            Assert.NotNull(result.DateTimeUpdated);
        }

        [Fact]
        public void Map_ValidResultUpdationDto_ReturnsResult()
        {
            // Arrange
            var resultUpdationDto = new ResultUpdationDto
            {
                AssessmentId = 2,
                EmployeeId = 2,
                Score = 100,
                DateTimeDue = "2023-11-15 10:30:00",
            };

            // Act
            var result = _mapper.Map<Result>(resultUpdationDto);

            // Assert
            Assert.Equal(resultUpdationDto.AssessmentId, result.AssessmentId);
            Assert.Equal(resultUpdationDto.EmployeeId, result.EmployeeId);
            Assert.Equal(resultUpdationDto.Score, result.Score);
            Assert.Equal(resultUpdationDto.DateTimeDue, result.DateTimeDue);
            Assert.NotNull(result.DateTimeUpdated);
        }
    }
}