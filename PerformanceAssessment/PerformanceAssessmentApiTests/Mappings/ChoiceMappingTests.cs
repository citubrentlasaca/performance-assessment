using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class ChoiceMappingTests
    {
        private readonly IMapper _mapper;

        public ChoiceMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new ChoiceMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidChoiceCreationDto_ReturnsChoice()
        {
            // Arrange
            var choiceCreationDto = new ChoiceCreationDto
            {
                ChoiceValue = "Evaluation",
                Weight = 25,
                ItemId = 1,
            };

            // Act
            var choice = _mapper.Map<Choice>(choiceCreationDto);

            // Assert
            Assert.Equal(choiceCreationDto.ChoiceValue, choice.ChoiceValue);
            Assert.Equal(choiceCreationDto.Weight, choice.Weight);
            Assert.Equal(choiceCreationDto.ItemId, choice.ItemId);
        }

        [Fact]
        public void Map_ValidChoiceUpdationDto_ReturnsChoice()
        {
            // Arrange
            var choiceUpdationDto = new ChoiceUpdationDto
            {
                ChoiceValue = "Analytics",
                Weight = 50,
            };

            // Act
            var choice = _mapper.Map<Choice>(choiceUpdationDto);

            // Assert
            Assert.Equal(choiceUpdationDto.ChoiceValue, choice.ChoiceValue);
            Assert.Equal(choiceUpdationDto.Weight, choice.Weight);
        }
    }
}