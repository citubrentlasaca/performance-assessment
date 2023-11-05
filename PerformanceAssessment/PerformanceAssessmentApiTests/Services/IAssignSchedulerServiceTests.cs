using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IAssignSchedulerServiceTests
    {
        private readonly Mock<IAssignSchedulerRepository> _fakeAssignSchedulerRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IAssignSchedulerService _assignSchedulerService;

        public IAssignSchedulerServiceTests()
        {
            _fakeAssignSchedulerRepository = new Mock<IAssignSchedulerRepository>();
            _fakeMapper = new Mock<IMapper>();
            _assignSchedulerService = new AssignSchedulerService(_fakeAssignSchedulerRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateAssignSchedulers_ValidData_ReturnsInsertedIds()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeIds = new List<int> { 1, 2, 3 };

            var assignSchedulerCreationDto = new AssignSchedulerCreationDto
            {
                AssessmentId = assessmentId,
                Occurrence = "Daily",
                DueDate = "2023-10-25",
                Time = "11:59"
            };

            var expectedInsertedIds = new List<int> { 4, 5, 6 };
            var assignScheduler = new AssignScheduler
            {
                AssessmentId = assignSchedulerCreationDto.AssessmentId,
                Occurrence = assignSchedulerCreationDto.Occurrence,
                DueDate = assignSchedulerCreationDto.DueDate,
                Time = assignSchedulerCreationDto.Time
            };

            _fakeMapper.Setup(x => x.Map<AssignScheduler>(assignSchedulerCreationDto)).Returns(assignScheduler);
            _fakeAssignSchedulerRepository.Setup(x => x.CreateAssignSchedulers(employeeIds, assignScheduler)).ReturnsAsync(expectedInsertedIds);

            // Act
            var insertedIds = await _assignSchedulerService.CreateAssignSchedulers(employeeIds, assignSchedulerCreationDto);

            // Assert
            Assert.NotNull(insertedIds);
            Assert.Equal(expectedInsertedIds, insertedIds);
        }

        [Fact]
        public async Task GetAllAssignSchedulers_ExistingAssignSchedulers_ReturnsAllAssignSchedulers()
        {
            // Arrange
            var schedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAssignSchedulers = new List<AssignSchedulerDto>
            {
                new AssignSchedulerDto
                {
                    Id = schedulerId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    IsAnswered = true,
                    Occurrence = "Daily",
                    DueDate = "2023-10-25",
                    Time = "11:59"
                },
                new AssignSchedulerDto
                {
                    Id = schedulerId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    IsAnswered = false,
                    Occurrence = "Daily",
                    DueDate = "2023-10:26",
                    Time = "11:00"
                },
            };

            _fakeAssignSchedulerRepository.Setup(x => x.GetAllAssignSchedulers()).ReturnsAsync(expectedAssignSchedulers);

            // Act
            var result = await _assignSchedulerService.GetAllAssignSchedulers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssignSchedulers.Count, result.Count());
        }

        [Fact]
        public async Task GetAssignSchedulerById_ExistingAssignScheduler_ReturnsAssignScheduler()
        {
            // Arrange
            var schedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAssignScheduler = new AssignSchedulerDto
            {
                Id = schedulerId,
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                IsAnswered = true,
                Occurrence = "Daily",
                DueDate = "2023-10-25",
                Time = "11:59"
            };

            _fakeAssignSchedulerRepository.Setup(x => x.GetAssignSchedulerById(schedulerId)).ReturnsAsync(expectedAssignScheduler);

            // Act
            var result = await _assignSchedulerService.GetAssignSchedulerById(schedulerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssignScheduler.Id, result.Id);
            Assert.Equal(expectedAssignScheduler.AssessmentId, result.AssessmentId);
            Assert.Equal(expectedAssignScheduler.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedAssignScheduler.IsAnswered, result.IsAnswered);
            Assert.Equal(expectedAssignScheduler.Occurrence, result.Occurrence);
            Assert.Equal(expectedAssignScheduler.DueDate, result.DueDate);
            Assert.Equal(expectedAssignScheduler.Time, result.Time);
        }

        [Fact]
        public async Task GetAssignSchedulerByAssessmentId_ExistingAssignSchedulers_ReturnsAssignSchedulers()
        {
            // Arrange
            var schedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAssignSchedulers = new List<AssignSchedulerDto>
            {
                new AssignSchedulerDto
                {
                    Id = schedulerId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    IsAnswered = true,
                    Occurrence = "Daily",
                    DueDate = "2023-10-25",
                    Time = "11:59"
                },
                new AssignSchedulerDto
                {
                    Id = schedulerId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    IsAnswered = false,
                    Occurrence = "Daily",
                    DueDate = "2023-10:26",
                    Time = "11:00"
                },
            };

            _fakeAssignSchedulerRepository.Setup(x => x.GetAssignSchedulerByAssessmentId(assessmentId)).ReturnsAsync(expectedAssignSchedulers);

            // Act
            var result = await _assignSchedulerService.GetAssignSchedulerByAssessmentId(assessmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssignSchedulers.Count, result.Count());
        }

        [Fact]
        public async Task GetAssignSchedulerByEmployeeId_ExistingAssignSchedulers_ReturnsAssignSchedulers()
        {
            // Arrange
            var schedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAssignSchedulers = new List<AssignSchedulerDto>
            {
                new AssignSchedulerDto
                {
                    Id = schedulerId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    IsAnswered = true,
                    Occurrence = "Daily",
                    DueDate = "2023-10-25",
                    Time = "11:59"
                },
                new AssignSchedulerDto
                {
                    Id = schedulerId,
                    AssessmentId = assessmentId,
                    EmployeeId = employeeId,
                    IsAnswered = false,
                    Occurrence = "Daily",
                    DueDate = "2023-10:26",
                    Time = "11:00"
                },
            };

            _fakeAssignSchedulerRepository.Setup(x => x.GetAssignSchedulerByEmployeeId(employeeId)).ReturnsAsync(expectedAssignSchedulers);

            // Act
            var result = await _assignSchedulerService.GetAssignSchedulerByEmployeeId(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssignSchedulers.Count, result.Count());
        }

        [Fact]
        public async Task UpdateAssignScheduler_ExistingAssignScheduler_ReturnsUpdatedCount()
        {
            // Arrange
            var schedulerId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var assignSchedulerUpdationDto = new AssignSchedulerUpdationDto
            {
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                IsAnswered = true,
                Occurrence = "Once",
                DueDate = "2023-10-31",
                Time = "12:00"
            };

            var assignScheduler = new AssignScheduler
            {
                AssessmentId = assignSchedulerUpdationDto.AssessmentId,
                Occurrence = assignSchedulerUpdationDto.Occurrence,
                DueDate = assignSchedulerUpdationDto.DueDate,
                Time = assignSchedulerUpdationDto.Time
            };

            _fakeMapper.Setup(x => x.Map<AssignScheduler>(assignSchedulerUpdationDto)).Returns(assignScheduler);
            _fakeAssignSchedulerRepository.Setup(x => x.UpdateAssignScheduler(assignScheduler)).ReturnsAsync(1);

            // Act
            var result = await _assignSchedulerService.UpdateAssignScheduler(schedulerId, assignSchedulerUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAssignScheduler_ExistingAssignScheduler_ReturnsDeletionCount()
        {
            // Arrange
            var schedulerId = It.IsAny<int>();

            _fakeAssignSchedulerRepository.Setup(x => x.DeleteAssignScheduler(schedulerId)).ReturnsAsync(1);

            // Act
            var result = await _assignSchedulerService.DeleteAssignScheduler(schedulerId);

            // Assert
            Assert.Equal(1, result);
        }

        //[Fact]
        //public async Task SetIsAnsweredToFalse_ExistingAssignScheduler_SetsIsAnsweredToFalse()
        //{
        //    // Arrange
        //    var schedulerId = It.IsAny<int>();

        //    var mockScheduler = new AssignSchedulerDto
        //    {
        //        Id = schedulerId,
        //        IsAnswered = true, // Set IsAnswered to true
        //                           // Initialize other properties as needed
        //    };

        //    _fakeAssignSchedulerRepository.Setup(repo => repo.GetAssignSchedulerById(schedulerId))
        //        .ReturnsAsync(mockScheduler);

        //    // Act
        //    await _assignSchedulerService.SetIsAnsweredToFalse(schedulerId);

        //    // Assert

        //    // Verify that GetAssignSchedulerById was called with the correct schedulerId
        //    _fakeAssignSchedulerRepository.Verify(repo => repo.GetAssignSchedulerById(schedulerId), Times.Once);

        //    // Verify that UpdateAssignScheduler was called with the updated scheduler
        //    _fakeAssignSchedulerRepository.Verify(repo => repo.UpdateAssignScheduler(new AssignScheduler()), Times.Once);

        //    // Assert that IsAnswered has been set to false
        //    Assert.False(mockScheduler.IsAnswered);
        //}

        [Fact]
        public async Task GetAssignSchedulerByEmployeeIdAndAssessmentId_ExistingAssignScheduler_ReturnsAssignScheduler()
        {
            // Arrange
            var schedulerId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();

            var expectedAssignScheduler = new AssignSchedulerDto
            {
                Id = schedulerId,
                AssessmentId = assessmentId,
                EmployeeId = employeeId,
                IsAnswered = true,
                Occurrence = "Daiy",
                DueDate = "2023-10-25",
                Time = "11:59"
            };

            _fakeAssignSchedulerRepository.Setup(x => x.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId)).ReturnsAsync(expectedAssignScheduler);

            // Act
            var result = await _assignSchedulerService.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssignScheduler.Id, result.Id);
            Assert.Equal(expectedAssignScheduler.AssessmentId, result.AssessmentId);
            Assert.Equal(expectedAssignScheduler.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedAssignScheduler.IsAnswered, result.IsAnswered);
            Assert.Equal(expectedAssignScheduler.Occurrence, result.Occurrence);
            Assert.Equal(expectedAssignScheduler.DueDate, result.DueDate);
            Assert.Equal(expectedAssignScheduler.Time, result.Time);
        }
    }
}