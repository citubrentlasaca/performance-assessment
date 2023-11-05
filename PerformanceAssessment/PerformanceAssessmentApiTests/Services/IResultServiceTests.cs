using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IResultServiceTests
    {
        private readonly Mock<IResultRepository> _fakeRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IResultService _resultService;

        public IResultServiceTests()
        {
            _fakeRepository = new Mock<IResultRepository>();
            _fakeMapper = new Mock<IMapper>();
            _resultService = new ResultService(_fakeRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateResult_ValidResultCreationDto_ReturnsCreatedResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultCreationDto = new ResultCreationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 50,
                DateTimeDue = "Wednesday, October 25, 2023"
            };

            var result = new Result
            {
                Id = resultId,
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 50,
                DateTimeDue = "Wednesday, October 25, 2023"
            };

            _fakeMapper.Setup(x => x.Map<Result>(resultCreationDto)).Returns(result);
            _fakeRepository.Setup(x => x.CreateResult(result)).ReturnsAsync(result.Id);

            // Act
            var createdResult = await _resultService.CreateResult(resultCreationDto);

            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal(result.Id, createdResult.Id);
            Assert.Equal(result.AssessmentId, createdResult.AssessmentId);
            Assert.Equal(result.EmployeeId, createdResult.EmployeeId);
            Assert.Equal(result.Score, createdResult.Score);
            Assert.Equal(result.DateTimeDue, createdResult.DateTimeDue);
        }

        [Fact]
        public async Task GetAllResults_ExistingResults_ReturnsAllResults()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var results = new List<ResultDto>
            {
                new ResultDto
                {
                    Id = resultId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    Score = 30,
                    DateTimeDue = "Friday, October 27, 2023"
                },
                new ResultDto
                {
                    Id = resultId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    Score = 60,
                    DateTimeDue = "Monday, October 30, 2023"
                },
            };

            _fakeRepository.Setup(x => x.GetAllResults()).ReturnsAsync(results);

            // Act
            var result = await _resultService.GetAllResults();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(results.Count, result.Count());
        }

        [Fact]
        public async Task GetResultById_ExistingResult_ReturnsResult()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultDto = new ResultDto
            {
                Id = resultId,
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 30,
                DateTimeDue = "Friday, October 27, 2023"
            };

            _fakeRepository.Setup(x => x.GetResultById(resultId)).ReturnsAsync(resultDto);

            // Act
            var result = await _resultService.GetResultById(resultId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(resultDto.Id, result.Id);
            Assert.Equal(resultDto.AssessmentId, result.AssessmentId);
            Assert.Equal(resultDto.EmployeeId, result.EmployeeId);
            Assert.Equal(resultDto.Score, result.Score);
            Assert.Equal(resultDto.DateTimeDue, result.DateTimeDue);
        }

        [Fact]
        public async Task GetResultByAssessmentId_ExistingResults_ReturnsResults()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var results = new List<ResultDto>
            {
                new ResultDto
                {
                    Id = resultId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    Score = 30,
                    DateTimeDue = "Friday, October 27, 2023"
                },
                new ResultDto
                {
                    Id = resultId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    Score = 60,
                    DateTimeDue = "Monday, October 30, 2023"
                },
            };

            _fakeRepository.Setup(x => x.GetResultByAssessmentId(assessmentId)).ReturnsAsync(results);

            // Act
            var result = await _resultService.GetResultByAssessmentId(assessmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(results.Count, result.Count());
        }

        [Fact]
        public async Task GetResultByEmployeeId_ExistingResults_ReturnsResults()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var results = new List<ResultDto>
            {
                new ResultDto
                {
                    Id = resultId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    Score = 30,
                    DateTimeDue = "Friday, October 27, 2023"
                },
                new ResultDto
                {
                    Id = resultId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    Score = 60,
                    DateTimeDue = "Monday, October 30, 2023"
                },
            };

            _fakeRepository.Setup(x => x.GetResultByEmployeeId(employeeId)).ReturnsAsync(results);

            // Act
            var result = await _resultService.GetResultByEmployeeId(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(results.Count, result.Count());
        }

        [Fact]
        public async Task UpdateResult_ExistingResult_ReturnsNumberOfUpdatedResults()
        {
            // Arrange
            var resultId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var resultUpdationDto = new ResultUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 70,
                DateTimeDue = "Monday, November 06, 2023"
            };

            var resultModel = new Result
            {
                Id = resultId,
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                Score = 70,
                DateTimeDue = "Monday, November 06, 2023"
            };

            _fakeMapper.Setup(x => x.Map<Result>(resultUpdationDto)).Returns(resultModel);
            _fakeRepository.Setup(x => x.UpdateResult(resultModel)).ReturnsAsync(1);

            // Act
            var result = await _resultService.UpdateResult(resultId, resultUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteResult_ExistingResult_ReturnsNumberOfDeletedResults()
        {
            // Arrange
            var resultId = It.IsAny<int>();

            _fakeRepository.Setup(x => x.DeleteResult(resultId)).ReturnsAsync(1);

            // Act
            var result = await _resultService.DeleteResult(resultId);

            // Assert
            Assert.Equal(1, result);
        }
    }
}