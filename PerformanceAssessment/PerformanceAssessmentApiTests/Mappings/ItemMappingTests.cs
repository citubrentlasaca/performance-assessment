using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class ItemMappingTests
    {
        private readonly IMapper _mapper;

        public ItemMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new ItemMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidItemCreationDto_ReturnsItem()
        {
            // Arrange
            var itemCreationDto = new ItemCreationDto
            {
                Question = "What can you say about the overall performance of your work?",
                QuestionType = "Short Answer",
                Weight = 50,
                Target = 25,
                Required = true,
                AssessmentId = 1,
            };

            // Act
            var item = _mapper.Map<Item>(itemCreationDto);

            // Assert
            Assert.Equal(itemCreationDto.Question, item.Question);
            Assert.Equal(itemCreationDto.QuestionType, item.QuestionType);
            Assert.Equal(itemCreationDto.Weight, item.Weight);
            Assert.Equal(itemCreationDto.Target, item.Target);
            Assert.Equal(itemCreationDto.Required, item.Required);
            Assert.Equal(itemCreationDto.AssessmentId, item.AssessmentId);
        }

        [Fact]
        public void Map_ValidItemUpdationDto_ReturnsItem()
        {
            // Arrange
            var itemUpdationDto = new ItemUpdationDto
            {
                Question = "What steps are you planning on taking to further improve your job performance before your next review?",
                QuestionType = "Paragraph",
                Weight = 100,
                Target = 50,
                Required = false,
            };

            // Act
            var item = _mapper.Map<Item>(itemUpdationDto);

            // Assert
            Assert.Equal(itemUpdationDto.Question, item.Question);
            Assert.Equal(itemUpdationDto.QuestionType, item.QuestionType);
            Assert.Equal(itemUpdationDto.Weight, item.Weight);
            Assert.Equal(itemUpdationDto.Target, item.Target);
            Assert.Equal(itemUpdationDto.Required, item.Required);
        }
    }
}