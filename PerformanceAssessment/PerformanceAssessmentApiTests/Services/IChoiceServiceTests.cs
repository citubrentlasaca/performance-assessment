using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IChoiceServiceTests
    {
        private readonly Mock<IChoiceRepository> _fakeChoiceRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IChoiceService _choiceService;

        public IChoiceServiceTests()
        {
            _fakeChoiceRepository = new Mock<IChoiceRepository>();
            _fakeMapper = new Mock<IMapper>();
            _choiceService = new ChoiceService(_fakeChoiceRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateChoice_ValidDto_ReturnsCreatedChoice()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var choiceId = It.IsAny<int>();

            var choiceCreationDto = new ChoiceCreationDto
            {
                ChoiceValue = "Evaluation",
                Weight = 25,
                ItemId = itemId
            };

            var choice = new Choice
            {
                Id = choiceId,
                ChoiceValue = choiceCreationDto.ChoiceValue,
                Weight = choiceCreationDto.Weight,
                ItemId = choiceCreationDto.ItemId
            };

            _fakeMapper.Setup(x => x.Map<Choice>(choiceCreationDto)).Returns(choice);
            _fakeChoiceRepository.Setup(x => x.CreateChoice(choice)).ReturnsAsync(choice.Id);

            // Act
            var createdChoice = await _choiceService.CreateChoice(choiceCreationDto);

            // Assert
            Assert.NotNull(createdChoice);
            Assert.Equal(choice.Id, createdChoice.Id);
            Assert.Equal(choice.ChoiceValue, createdChoice.ChoiceValue);
            Assert.Equal(choice.Weight, createdChoice.Weight);
            Assert.Equal(choice.ItemId, createdChoice.ItemId);
        }

        [Fact]
        public async Task GetAllChoices_ExistingChoices_ReturnsAllChoices()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var choiceId = It.IsAny<int>();

            var choices = new List<ChoiceDto>
            {
                new ChoiceDto
                {
                    Id = choiceId,
                    ChoiceValue = "Evaluation",
                    Weight = 25,
                    ItemId = itemId
                },
                new ChoiceDto
                {
                    Id = choiceId,
                    ChoiceValue = "Analytics",
                    Weight = 25,
                    ItemId = itemId
                },
            };

            _fakeChoiceRepository.Setup(x => x.GetAllChoices()).ReturnsAsync(choices);

            // Act
            var result = await _choiceService.GetAllChoices();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(choices.Count, result.Count());
        }

        [Fact]
        public async Task GetChoiceById_ExistingChoice_ReturnsChoiceDto()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var choiceId = It.IsAny<int>();

            var expectedChoiceDto = new ChoiceDto
            {
                Id = choiceId,
                ChoiceValue = "Evaluation",
                Weight = 25,
                ItemId = itemId
            };

            _fakeChoiceRepository.Setup(x => x.GetChoiceById(choiceId)).ReturnsAsync(expectedChoiceDto);

            // Act
            var result = await _choiceService.GetChoiceById(choiceId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedChoiceDto.Id, result.Id);
            Assert.Equal(expectedChoiceDto.ChoiceValue, result.ChoiceValue);
            Assert.Equal(expectedChoiceDto.Weight, result.Weight);
            Assert.Equal(expectedChoiceDto.ItemId, result.ItemId);
        }

        [Fact]
        public async Task UpdateChoice_ExistingChoice_ReturnsUpdatedChoice()
        {
            // Arrange
            var choiceId = It.IsAny<int>();

            var choiceUpdationDto = new ChoiceUpdationDto
            {
                ChoiceValue = "Efficiency",
                Weight = 50
            };

            var choice = new Choice
            {
                Id = choiceId,
                ChoiceValue = choiceUpdationDto.ChoiceValue,
                Weight = choiceUpdationDto.Weight,
            };

            _fakeMapper.Setup(x => x.Map<Choice>(choiceUpdationDto)).Returns(choice);
            _fakeChoiceRepository.Setup(x => x.UpdateChoice(choice)).ReturnsAsync(1);

            // Act
            var updatedChoiceId = await _choiceService.UpdateChoice(choiceId, choiceUpdationDto);

            // Assert
            Assert.Equal(1, updatedChoiceId);
        }

        [Fact]
        public async Task DeleteChoice_ExistingChoice_ReturnsDeletedChoiceId()
        {
            // Arrange
            var choiceId = It.IsAny<int>();

            _fakeChoiceRepository.Setup(x => x.DeleteChoice(choiceId)).ReturnsAsync(1);

            // Act
            var deletedChoiceId = await _choiceService.DeleteChoice(choiceId);

            // Assert
            Assert.Equal(1, deletedChoiceId);
        }
    }
}