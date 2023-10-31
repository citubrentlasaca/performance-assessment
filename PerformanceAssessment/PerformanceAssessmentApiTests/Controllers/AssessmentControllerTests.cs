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
    public class AssessmentControllerTests
    {
        private readonly AssessmentController _controller;
        private readonly Mock<IAssessmentService> _fakeAssessmentService;
        private readonly Mock<ILogger<AssessmentController>> _logger;

        public AssessmentControllerTests()
        {
            _fakeAssessmentService = new Mock<IAssessmentService>();
            _logger = new Mock<ILogger<AssessmentController>>();

            _controller = new AssessmentController(_fakeAssessmentService.Object, _logger.Object);
        }

        [Fact]
        public async Task CreateAssessment_ValidData_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            var assessmentCreationDto = new AssessmentCreationDto
            {
                EmployeeId = employeeId,
                Title = "Software Engineering 1",
                Description = "SPMP"
            };
            _fakeAssessmentService.Setup(service => service.CreateAssessment(assessmentCreationDto))
                .ReturnsAsync(new Assessment());

            // Act
            var result = await _controller.CreateAssessment(assessmentCreationDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateAssessment_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            var assessmentCreationDto = new AssessmentCreationDto
            {
                EmployeeId = employeeId,
                Title = "Software Engineering 1",
                Description = "SPMP"
            };
            _fakeAssessmentService.Setup(service => service.CreateAssessment(assessmentCreationDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateAssessment(assessmentCreationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllAssessments_HasAssessments_ReturnsOkObjectResult()
        {
            // Arrange
            var assessments = new List<AssessmentDto> { new AssessmentDto() };

            _fakeAssessmentService.Setup(service => service.GetAllAssessments())
                .ReturnsAsync(assessments);

            // Act
            var result = await _controller.GetAllAssessments();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllAssessments_HasNoAssessments_ReturnsNoContentResult()
        {
            // Arrange
            _fakeAssessmentService.Setup(service => service.GetAllAssessments())
                .ReturnsAsync(new List<AssessmentDto>());

            // Act
            var result = await _controller.GetAllAssessments();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllAssessments_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeAssessmentService.Setup(service => service.GetAllAssessments())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllAssessments();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssessmentById_ExistingAssessment_ReturnsOkObjectResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var assessmentDto = new AssessmentDto { Id = assessmentId };

            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync(assessmentDto);

            // Act
            var result = await _controller.GetAssessmentById(assessmentId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAssessmentById_HasNoAssessment_ReturnsNotFoundResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync((AssessmentDto?)null!);

            // Act
            var result = await _controller.GetAssessmentById(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssessmentById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssessmentById(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateAssessment_ExistingAssessment_ReturnsOkObjectResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            var assessmentUpdationDto = new AssessmentUpdationDto
            {
                Title = "Programming Languages",
                Description = "History and Uses Of The Programming Languages"
            };
            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync(new AssessmentDto { Id = assessmentId });
            _fakeAssessmentService.Setup(service => service.UpdateAssessment(assessmentId, assessmentUpdationDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateAssessment(assessmentId, assessmentUpdationDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAssessment_HasNoAssessment_ReturnsNotFoundResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            var assessmentUpdationDto = new AssessmentUpdationDto
            {
                Title = "Programming Languages",
                Description = "History and Uses Of The Programming Languages"
            };
            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync((AssessmentDto?)null!);

            // Act
            var result = await _controller.UpdateAssessment(assessmentId, assessmentUpdationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateAssessment_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            var assessmentUpdationDto = new AssessmentUpdationDto
            {
                Title = "Programming Languages",
                Description = "History and Uses Of The Programming Languages"
            };
            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync(new AssessmentDto { Id = assessmentId });
            _fakeAssessmentService.Setup(service => service.UpdateAssessment(assessmentId, assessmentUpdationDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateAssessment(assessmentId, assessmentUpdationDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAssessment_ExistingAssessment_ReturnsOkObjectResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync(new AssessmentDto { Id = assessmentId });
            _fakeAssessmentService.Setup(service => service.DeleteAssessment(assessmentId))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteAssessment(assessmentId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAssessment_HasNoAssessment_ReturnsNotFoundResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync((AssessmentDto?)null!);

            // Act
            var result = await _controller.DeleteAssessment(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAssessment_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentById(assessmentId))
                .ReturnsAsync(new AssessmentDto { Id = assessmentId });
            _fakeAssessmentService.Setup(service => service.DeleteAssessment(assessmentId))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteAssessment(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssessmentItemsById_ExistingAssessment_ReturnsOkObjectResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            var assessmentItems = new AssessmentItemDto();
            _fakeAssessmentService.Setup(service => service.GetAssessmentItemsById(assessmentId))
                .ReturnsAsync(assessmentItems);

            // Act
            var result = await _controller.GetAssessmentItemsById(assessmentId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAssessmentItemsById_HasNoAssessmentItems_ReturnsNotFoundResult()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentItemsById(assessmentId))
                .ReturnsAsync(await Task.FromResult<AssessmentItemDto>(null!));

            // Act
            var result = await _controller.GetAssessmentItemsById(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssessmentItemsById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentItemsById(assessmentId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssessmentItemsById(assessmentId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAssessmentsByEmployeeId_ExistingAssessments_ReturnsOkObjectResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            var assessments = new List<AssessmentDto> { new AssessmentDto() };
            _fakeAssessmentService.Setup(service => service.GetAssessmentsByEmployeeId(employeeId))
                .ReturnsAsync(assessments);

            // Act
            var result = await _controller.GetAssessmentsByEmployeeId(employeeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAssessmentsByEmployeeId_HasNoAssessments_ReturnsNoContentResult()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentsByEmployeeId(employeeId))
                .ReturnsAsync(new List<AssessmentDto>());

            // Act
            var result = await _controller.GetAssessmentsByEmployeeId(employeeId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAssessmentsByEmployeeId_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeId = It.IsAny<int>();

            _fakeAssessmentService.Setup(service => service.GetAssessmentsByEmployeeId(employeeId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAssessmentsByEmployeeId(employeeId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}