using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceAssessmentApi.Controllers;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Controllers
{
    public class ResultControllerTests
    {
        private readonly ResultController _controller;
        private readonly Mock<IResultService> _fakeResultService;
        private readonly Mock<ILogger<ResultController>> _fakeLogger;

        public ResultControllerTests()
        {
            _fakeResultService = new Mock<IResultService>();
            _fakeLogger = new Mock<ILogger<ResultController>>();
            _controller = new ResultController(_fakeResultService.Object, _fakeLogger.Object);
        }

        [Fact]
        public async Task CreateResult_ValidResult_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultDto = new ResultCreationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 50,
                DateTimeDue = "Monday, October 16, 2023"
            };

            _fakeResultService.Setup(service => service.CreateResult(resultDto))
                .ReturnsAsync(new Result());

            // Act
            var result = await _controller.CreateResult(resultDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateResult_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultDto = new ResultCreationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 50,
                DateTimeDue = "Monday, October 16, 2023"
            };

            _fakeResultService.Setup(service => service.CreateResult(resultDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateResult(resultDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllResults_ExistingResults_ReturnsOkObjectResult()
        {
            // Arrange
            var results = new List<ResultDto>
            {
                new ResultDto(),
                new ResultDto()
            };

            _fakeResultService.Setup(service => service.GetAllResults())
                .ReturnsAsync(results);

            // Act
            var result = await _controller.GetAllResults();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllResults_HasNoResults_ReturnsNoContentResult()
        {
            // Arrange
            _fakeResultService.Setup(service => service.GetAllResults())
                .ReturnsAsync(new List<ResultDto>());

            // Act
            var result = await _controller.GetAllResults();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllResults_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeResultService.Setup(service => service.GetAllResults())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllResults();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetResultById_ExistingResult_ReturnsOkObjectResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var resultDto = new ResultDto { Id = resultId };

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync(resultDto);

            // Act
            var result = await _controller.GetResultById(resultId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetResultById_MissingResult_ReturnsNotFoundResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync((ResultDto)null!);

            // Act
            var result = await _controller.GetResultById(resultId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetResultById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var resultId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetResultById(resultId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetResultByAssessmentId_ExistingResult_ReturnsOkObjectResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var resultDto = new List<ResultDto> { new ResultDto() };

            _fakeResultService.Setup(service => service.GetResultByAssessmentId(assessmentId))
                .ReturnsAsync(resultDto);

            // Act
            var result = await _controller.GetResultByAssessmentId(assessmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetResultByAssessmentId_MissingResult_ReturnsNotFoundResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultByAssessmentId(assessmentId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<ResultDto>>(null!));

            // Act
            var result = await _controller.GetResultByAssessmentId(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetResultByAssessmentId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultByAssessmentId(assessmentId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetResultByAssessmentId(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetResultByEmployeeId_ExistingResult_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();
            var resultDto = new List<ResultDto> { new ResultDto() };

            _fakeResultService.Setup(service => service.GetResultByEmployeeId(employeeId))
                .ReturnsAsync(resultDto);

            // Act
            var result = await _controller.GetResultByEmployeeId(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetResultByEmployeeId_MissingResult_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultByEmployeeId(employeeId))
                .ReturnsAsync(await Task.FromResult<IEnumerable<ResultDto>>(null!));

            // Act
            var result = await _controller.GetResultByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetResultByEmployeeId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultByEmployeeId(employeeId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetResultByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateResult_ExistingResult_ReturnsOkObjectResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultDto = new ResultUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 100,
                DateTimeDue = "Tuesday, October 17, 2023"
            };

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync(new ResultDto());

            _fakeResultService.Setup(service => service.UpdateResult(resultId, resultDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateResult(resultId, resultDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateResult_MissingResult_ReturnsNotFoundResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultDto = new ResultUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 100,
                DateTimeDue = "Tuesday, October 17, 2023"
            };

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync((ResultDto)null!);

            // Act
            var result = await _controller.UpdateResult(resultId, resultDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateResult_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultDto = new ResultUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 100,
                DateTimeDue = "Tuesday, October 17, 2023"
            };

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync(new ResultDto());

            _fakeResultService.Setup(service => service.UpdateResult(resultId, resultDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateResult(resultId, resultDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteResult_ExistingResult_ReturnsOkObjectResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync(new ResultDto());

            _fakeResultService.Setup(service => service.DeleteResult(resultId))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteResult(resultId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteResult_MissingResult_ReturnsNotFoundResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync((ResultDto)null!);

            // Act
            var result = await _controller.DeleteResult(resultId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteResult_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var resultId = It.IsAny<int>();

            _fakeResultService.Setup(service => service.GetResultById(resultId))
                .ReturnsAsync(new ResultDto());

            _fakeResultService.Setup(service => service.DeleteResult(resultId))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteResult(resultId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}