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
    public class ChoiceControllerTests
    {
        private readonly ChoiceController _controller;
        private readonly Mock<IChoiceService> _fakeChoiceService;
        private readonly Mock<ILogger<ChoiceController>> _logger;

        public ChoiceControllerTests()
        {
            _fakeChoiceService = new Mock<IChoiceService>();
            _logger = new Mock<ILogger<ChoiceController>>();

            _controller = new ChoiceController(_fakeChoiceService.Object, _logger.Object);
        }

        [Fact]
        public async Task CreateChoice_ValidChoice_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            var choiceCreationDto = new ChoiceCreationDto
            {
                ChoiceValue = "Evaluation",
                Weight = 100,
                ItemId = itemId
            };

            _fakeChoiceService.Setup(service => service.CreateChoice(choiceCreationDto))
                .ReturnsAsync(new Choice());

            // Act
            var result = await _controller.CreateChoice(choiceCreationDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateChoice_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            var choiceCreationDto = new ChoiceCreationDto
            {
                ChoiceValue = "Evaluation",
                Weight = 100,
                ItemId = itemId
            };

            _fakeChoiceService.Setup(service => service.CreateChoice(choiceCreationDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateChoice(choiceCreationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllChoices_ExistingChoices_ReturnsOkObjectResult()
        {
            // Arrange
            _fakeChoiceService.Setup(service => service.GetAllChoices())
                .ReturnsAsync(new List<ChoiceDto> { new ChoiceDto() });

            // Act
            var result = await _controller.GetAllChoices();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllChoices_HasNoChoices_ReturnsNoContentResult()
        {
            // Arrange
            _fakeChoiceService.Setup(service => service.GetAllChoices())
                .ReturnsAsync(new List<ChoiceDto>());

            // Act
            var result = await _controller.GetAllChoices();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllChoices_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeChoiceService.Setup(service => service.GetAllChoices())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllChoices();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetChoiceById_ExistingChoice_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync(new ChoiceDto());

            // Act
            var result = await _controller.GetChoiceById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetChoiceById_MissingChoice_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync((ChoiceDto)null!);

            // Act
            var result = await _controller.GetChoiceById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetChoiceById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetChoiceById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateChoice_ExistingChoice_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            var choiceUpdationDto = new ChoiceUpdationDto
            {
                ChoiceValue = "Analytics",
                Weight = 50
            };

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync(new ChoiceDto());
            _fakeChoiceService.Setup(service => service.UpdateChoice(id, choiceUpdationDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateChoice(id, choiceUpdationDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateChoice_MissingChoice_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            var choiceUpdationDto = new ChoiceUpdationDto
            {
                ChoiceValue = "Analytics",
                Weight = 50
            };

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync((ChoiceDto)null!);

            // Act
            var result = await _controller.UpdateChoice(id, choiceUpdationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateChoice_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            var choiceUpdationDto = new ChoiceUpdationDto
            {
                ChoiceValue = "Analytics",
                Weight = 50
            };

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync(new ChoiceDto());
            _fakeChoiceService.Setup(service => service.UpdateChoice(id, choiceUpdationDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateChoice(id, choiceUpdationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteChoice_ExistingChoice_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync(new ChoiceDto());
            _fakeChoiceService.Setup(service => service.DeleteChoice(id))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteChoice(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteChoice_MissingChoice_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync((ChoiceDto)null!);

            // Act
            var result = await _controller.DeleteChoice(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteChoice_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeChoiceService.Setup(service => service.GetChoiceById(id))
                .ReturnsAsync(new ChoiceDto());
            _fakeChoiceService.Setup(service => service.DeleteChoice(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteChoice(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}