using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class AnswerMappingTests
    {
        private readonly IMapper _mapper;

        public AnswerMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AnswerMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidAnswerCreationDto_ReturnsAnswer()
        {
            // Arrange
            var answerCreationDto = new AnswerCreationDto
            {
                EmployeeId = 1,
                ItemId = 1,
                AnswerText = "My performance was great.",
                SelectedChoices = "Efficiency, Teamwork",
                CounterValue = 10,
            };

            // Act
            var answer = _mapper.Map<Answer>(answerCreationDto);

            // Assert
            Assert.Equal(answerCreationDto.EmployeeId, answer.EmployeeId);
            Assert.Equal(answerCreationDto.ItemId, answer.ItemId);
            Assert.Equal(answerCreationDto.AnswerText, answer.AnswerText);
            Assert.Equal(answerCreationDto.SelectedChoices, answer.SelectedChoices);
            Assert.Equal(answerCreationDto.CounterValue, answer.CounterValue);
            Assert.NotNull(answer.DateTimeAnswered);
        }

        [Fact]
        public void Map_ValidAnswerUpdationDto_ReturnsAnswer()
        {
            // Arrange
            var answerUpdationDto = new AnswerUpdationDto
            {
                EmployeeId = 2,
                ItemId = 2,
                AnswerText = "The team was good.",
                SelectedChoices = "Punctuality, Evaluation",
                CounterValue = 15,
            };

            // Act
            var answer = _mapper.Map<Answer>(answerUpdationDto);

            // Assert
            Assert.Equal(answerUpdationDto.EmployeeId, answer.EmployeeId);
            Assert.Equal(answerUpdationDto.ItemId, answer.ItemId);
            Assert.Equal(answerUpdationDto.AnswerText, answer.AnswerText);
            Assert.Equal(answerUpdationDto.SelectedChoices, answer.SelectedChoices);
            Assert.Equal(answerUpdationDto.CounterValue, answer.CounterValue);
            Assert.Null(answer.DateTimeAnswered);
        }
    }
}