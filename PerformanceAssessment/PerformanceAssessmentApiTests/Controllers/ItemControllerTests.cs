using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PerformanceAssessmentApi.Controllers;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Controllers
{
    public class ItemControllerTests
    {
        private readonly ItemController _controller;
        private readonly Mock<IItemService> _fakeItemService;
        private readonly Mock<ILogger<ItemController>> _fakeLogger;

        public ItemControllerTests()
        {
            _fakeItemService = new Mock<IItemService>();
            _fakeLogger = new Mock<ILogger<ItemController>>();
            _controller = new ItemController(_fakeItemService.Object, _fakeLogger.Object);
        }

        [Fact]
        public async Task CreateItem_ValidItem_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            var itemDto = new ItemCreationDto
            {
                Question = "What can you say about the overall performance of your work?",
                QuestionType = "Short Answer",
                Weight = 100,
                Target = 100,
                Required = true,
                AssessmentId = assessmentId
            };

            _fakeItemService.Setup(service => service.CreateItem(itemDto))
                .ReturnsAsync(new Item());

            // Act
            var result = await _controller.CreateItem(itemDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateItem_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            var itemDto = new ItemCreationDto
            {
                Question = "What can you say about the overall performance of your work?",
                QuestionType = "Short Answer",
                Weight = 100,
                Target = 100,
                Required = true,
                AssessmentId = assessmentId
            };

            _fakeItemService.Setup(service => service.CreateItem(itemDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateItem(itemDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllItems_ExistingItems_ReturnsOkObjectResult()
        {
            // Arrange
            var items = new List<ItemChoiceDto>
            {
                new ItemChoiceDto(),
                new ItemChoiceDto()
            };

            _fakeItemService.Setup(service => service.GetAllItems())
                .ReturnsAsync(items);

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllItems_HasNoItems_ReturnsNoContentResult()
        {
            // Arrange
            _fakeItemService.Setup(service => service.GetAllItems())
                .ReturnsAsync(new List<ItemChoiceDto>());

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllItems_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeItemService.Setup(service => service.GetAllItems())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetItemById_ExistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var item = new ItemChoiceDto { Id = itemId };

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync(item);

            // Act
            var result = await _controller.GetItemById(itemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetItemById_MissingItem_ReturnsNotFoundResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync((ItemChoiceDto)null!);

            // Act
            var result = await _controller.GetItemById(itemId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetItemById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetItemById(itemId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateItem_ExistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var itemDto = new ItemUpdationDto
            {
                Question = "What steps are you planning on taking to further improve your job performance before your next review?",
                QuestionType = "Paragraph",
                Weight = 90,
                Target = 50,
                Required = false
            };

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync(new ItemChoiceDto { Id = itemId });

            _fakeItemService.Setup(service => service.UpdateItem(itemId, itemDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateItem(itemId, itemDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateItem_MissingItem_ReturnsNotFoundResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var itemDto = new ItemUpdationDto
            {
                Question = "What steps are you planning on taking to further improve your job performance before your next review?",
                QuestionType = "Paragraph",
                Weight = 90,
                Target = 50,
                Required = false
            };

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync((ItemChoiceDto)null!);

            // Act
            var result = await _controller.UpdateItem(itemId, itemDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateItem_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var itemId = It.IsAny<int>();
            var itemDto = new ItemUpdationDto
            {
                Question = "What steps are you planning on taking to further improve your job performance before your next review?",
                QuestionType = "Paragraph",
                Weight = 90,
                Target = 50,
                Required = false
            };

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync(new ItemChoiceDto { Id = itemId });

            _fakeItemService.Setup(service => service.UpdateItem(itemId, itemDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateItem(itemId, itemDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteItem_ExistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync(new ItemChoiceDto { Id = itemId });

            _fakeItemService.Setup(service => service.DeleteItem(itemId))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteItem(itemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteItem_MissingItem_ReturnsNotFoundResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync((ItemChoiceDto)null!);

            // Act
            var result = await _controller.DeleteItem(itemId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteItem_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeItemService.Setup(service => service.GetItemById(itemId))
                .ReturnsAsync(new ItemChoiceDto { Id = itemId });

            _fakeItemService.Setup(service => service.DeleteItem(itemId))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteItem(itemId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}