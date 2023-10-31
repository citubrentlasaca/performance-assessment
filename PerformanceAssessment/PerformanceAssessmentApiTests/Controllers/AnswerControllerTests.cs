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
    public class AnswerControllerTests
    {
        private readonly AnswerController _controller;
        private readonly Mock<IAnswerService> _fakeAnswerService;
        private readonly Mock<ILogger<AnswerController>> _logger;

        public AnswerControllerTests()
        {
            _fakeAnswerService = new Mock<IAnswerService>();
            _logger = new Mock<ILogger<AnswerController>>();

            _controller = new AnswerController(_fakeAnswerService.Object, _logger.Object);
        }

        [Fact]
        public async Task SaveAnswers_ValidAnswers_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var answerCreationDto = new AnswerCreationDto
            {
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "My performance was great.",
                SelectedChoices = "Efficiency, Teamwork",
                CounterValue = 10
            };
            _fakeAnswerService.Setup(service => service.SaveAnswers(answerCreationDto))
                .ReturnsAsync(new Answer());

            // Act
            var result = await _controller.SaveAnswers(answerCreationDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task SaveAnswers_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var answerCreationDto = new AnswerCreationDto
            {
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "My performance was great.",
                SelectedChoices = "Efficiency, Teamwork",
                CounterValue = 10
            };
            _fakeAnswerService.Setup(service => service.SaveAnswers(answerCreationDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.SaveAnswers(answerCreationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAnswersByItemId_ExistingAnswers_ReturnsOkObjectResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersByItemId(itemId))
                .ReturnsAsync(new List<AnswerDto> { new AnswerDto() });

            // Act
            var result = await _controller.GetAnswersByItemId(itemId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAnswersByItemId_HasNoAnswers_ReturnsNotFoundResult()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersByItemId(itemId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<AnswerDto>>(null!));

            // Act
            var result = await _controller.GetAnswersByItemId(itemId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAnswersByItemId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var itemId = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersByItemId(itemId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAnswersByItemId(itemId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAnswersById_ExistingAnswers_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync(new AnswerDto());

            // Act
            var result = await _controller.GetAnswersById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAnswersById_MissingAnswers_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync((AnswerDto?)null!);

            // Act
            var result = await _controller.GetAnswersById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAnswersById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAnswersById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateAnswers_ExistingAnswers_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var answerUpdationDto = new AnswerUpdationDto
            {
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "The team was good.",
                SelectedChoices = "Punctuality, Evaluation",
                CounterValue = 5
            };

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync(new AnswerDto());
            _fakeAnswerService.Setup(service => service.UpdateAnswers(id, answerUpdationDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateAnswers(id, answerUpdationDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAnswers_MissingAnswers_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var answerUpdationDto = new AnswerUpdationDto
            {
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "The team was good.",
                SelectedChoices = "Punctuality, Evaluation",
                CounterValue = 5
            };

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync((AnswerDto?)null!);

            // Act
            var result = await _controller.UpdateAnswers(id, answerUpdationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateAnswers_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var answerUpdationDto = new AnswerUpdationDto
            {
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "The team was good.",
                SelectedChoices = "Punctuality, Evaluation",
                CounterValue = 5
            };

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync(new AnswerDto());
            _fakeAnswerService.Setup(service => service.UpdateAnswers(id, answerUpdationDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateAnswers(id, answerUpdationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAnswers_ExistingAnswers_ReturnsOkObjectResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync(new AnswerDto());
            _fakeAnswerService.Setup(service => service.DeleteAnswers(id))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteAnswers(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAnswers_MissingAnswers_ReturnsNotFoundResult()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync((AnswerDto?)null!);

            // Act
            var result = await _controller.DeleteAnswers(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAnswers_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var id = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAnswersById(id))
                .ReturnsAsync(new AnswerDto());
            _fakeAnswerService.Setup(service => service.DeleteAnswers(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteAnswers(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssessmentAnswersByEmployeeIdAndAssessmentId_ExistingAnswers_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId))
                .ReturnsAsync(new AssessmentAnswersDto());

            // Act
            var result = await _controller.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAssessmentAnswersByEmployeeIdAndAssessmentId_HasNoAnswers_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId))
                .ReturnsAsync((AssessmentAnswersDto?)null!);

            // Act
            var result = await _controller.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssessmentAnswersByEmployeeIdAndAssessmentId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            _fakeAnswerService.Setup(service => service.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}