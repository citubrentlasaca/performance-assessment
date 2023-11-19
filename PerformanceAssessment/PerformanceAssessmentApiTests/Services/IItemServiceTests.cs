using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IItemServiceTests
    {
        private readonly Mock<IItemRepository> _fakeRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IItemService _itemService;

        public IItemServiceTests()
        {
            _fakeRepository = new Mock<IItemRepository>();
            _fakeMapper = new Mock<IMapper>();
            _itemService = new ItemService(_fakeRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateItem_ValidItemCreationDto_ReturnsCreatedItem()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            var itemCreationDto = new ItemCreationDto
            {
                Question = "What can you say about the overall performance of your work?",
                QuestionType = "Short Answer",
                Weight = 25,
                Target = 25,
                Required = true,
                AssessmentId = 1
            };

            var item = new Item
            {
                Id = itemId,
                Question = "What can you say about the overall performance of your work?",
                QuestionType = "Short Answer",
                Weight = 25,
                Target = 25,
                Required = true,
                AssessmentId = 1
            };

            _fakeMapper.Setup(x => x.Map<Item>(itemCreationDto)).Returns(item);
            _fakeRepository.Setup(x => x.CreateItem(item)).ReturnsAsync(item.Id);

            // Act
            var createdItem = await _itemService.CreateItem(itemCreationDto);

            // Assert
            Assert.NotNull(createdItem);
            Assert.Equal(item.Id, createdItem.Id);
            Assert.Equal(item.Question, createdItem.Question);
            Assert.Equal(item.QuestionType, createdItem.QuestionType);
            Assert.Equal(item.Weight, createdItem.Weight);
            Assert.Equal(item.Target, createdItem.Target);
            Assert.Equal(item.Required, createdItem.Required);
            Assert.Equal(item.AssessmentId, createdItem.AssessmentId);
        }

        [Fact]
        public async Task GetAllItems_ExistingItems_ReturnsAllItems()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var items = new List<ItemChoiceDto>
            {
                new ItemChoiceDto
                {
                    Id = itemId,
                    Question = "What can you say about the overall performance of your work?",
                    QuestionType = "Short Answer",
                    Weight = 25,
                    Target = 25,
                    Required = true,
                    AssessmentId = assessmentId
                },
                new ItemChoiceDto
                {
                    Id = itemId,
                    Question = "What steps are you planning on taking to further improve your job performance before your next review?",
                    QuestionType = "Paragraph",
                    Weight = 30,
                    Target = 30,
                    Required = true,
                    AssessmentId = assessmentId
                },
            };

            _fakeRepository.Setup(x => x.GetAllItems()).ReturnsAsync(items);

            // Act
            var result = await _itemService.GetAllItems();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(items.Count, result.Count());
        }

        [Fact]
        public async Task GetItemById_ExistingItem_ReturnsItem()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var itemDto = new ItemChoiceDto
            {
                Id = itemId,
                Question = "What can you say about the overall performance of your work?",
                QuestionType = "Short Answer",
                Weight = 25,
                Target = 25,
                Required = true,
                AssessmentId = assessmentId
            };

            _fakeRepository.Setup(x => x.GetItemById(itemId)).ReturnsAsync(itemDto);

            // Act
            var item = await _itemService.GetItemById(itemId);

            // Assert
            Assert.NotNull(item);
            Assert.Equal(itemDto.Id, item.Id);
            Assert.Equal(itemDto.Question, item.Question);
            Assert.Equal(itemDto.QuestionType, item.QuestionType);
            Assert.Equal(itemDto.Weight, item.Weight);
            Assert.Equal(itemDto.Target, item.Target);
            Assert.Equal(itemDto.Required, item.Required);
            Assert.Equal(itemDto.AssessmentId, item.AssessmentId);
        }

        [Fact]
        public async Task UpdateItem_ExistingItem_ReturnsNumberOfUpdatedItems()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            var itemUpdationDto = new ItemUpdationDto
            {
                Question = "How do you adapt to changes in your work environment and responsibilities?",
                QuestionType = "Paragraph",
                Weight = 75,
                Target = 75,
                Required = false
            };

            var item = new Item
            {
                Id = itemId,
                Question = "How do you adapt to changes in your work environment and responsibilities?",
                QuestionType = "Paragraph",
                Weight = 75,
                Target = 75,
                Required = false
            };

            _fakeMapper.Setup(x => x.Map<Item>(itemUpdationDto)).Returns(item);
            _fakeRepository.Setup(x => x.UpdateItem(item)).ReturnsAsync(1);

            // Act
            var result = await _itemService.UpdateItem(itemId, itemUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteItem_ExistingItem_ReturnsNumberOfDeletedItems()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeRepository.Setup(x => x.DeleteItem(itemId)).ReturnsAsync(1);

            // Act
            var result = await _itemService.DeleteItem(itemId);

            // Assert
            Assert.Equal(1, result);
        }
    }
}