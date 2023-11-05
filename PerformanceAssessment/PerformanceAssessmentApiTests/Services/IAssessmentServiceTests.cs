using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IAssessmentServiceTests
    {
        private readonly Mock<IAssessmentRepository> _fakeAssessmentRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IAssessmentService _assessmentService;

        public IAssessmentServiceTests()
        {
            _fakeAssessmentRepository = new Mock<IAssessmentRepository>();
            _fakeMapper = new Mock<IMapper>();
            _assessmentService = new AssessmentService(_fakeAssessmentRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateAssessment_ValidData_ReturnsCreatedAssessment()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var assessmentCreationDto = new AssessmentCreationDto
            {
                EmployeeId = employeeId,
                Title = "Daily Performance Report",
                Description = "Analysis of daily work performance"
            };

            var expectedAssessment = new Assessment
            {
                Id = assessmentId,
                EmployeeId = assessmentCreationDto.EmployeeId,
                Title = assessmentCreationDto.Title,
                Description = assessmentCreationDto.Description,
            };

            _fakeMapper.Setup(x => x.Map<Assessment>(assessmentCreationDto)).Returns(expectedAssessment);
            _fakeAssessmentRepository.Setup(x => x.CreateAssessment(expectedAssessment)).ReturnsAsync(assessmentId);

            // Act
            var result = await _assessmentService.CreateAssessment(assessmentCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(assessmentId, result.Id);
            Assert.Equal(assessmentCreationDto.EmployeeId, result.EmployeeId);
            Assert.Equal(assessmentCreationDto.Title, result.Title);
            Assert.Equal(assessmentCreationDto.Description, result.Description);
        }

        [Fact]
        public async Task GetAllAssessments_ExistingAssessments_ReturnsAllAssessments()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAssessments = new List<AssessmentDto>
            {
                new AssessmentDto
                {
                    Id = assessmentId,
                    EmployeeId = employeeId,
                    Title = "Daily Performance Report",
                    Description = "Analysis of daily work performance"
                },
                new AssessmentDto
                {
                    Id = assessmentId,
                    EmployeeId = employeeId,
                    Title = "Quarterly Performance Review",
                    Description = "Assessment progress report for Q3 2023"
                },
            };

            _fakeAssessmentRepository.Setup(x => x.GetAllAssessments()).ReturnsAsync(expectedAssessments);

            // Act
            var result = await _assessmentService.GetAllAssessments();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssessments.Count, result.Count());
        }

        [Fact]
        public async Task GetAssessmentById_ExistingAssessment_ReturnsAssessment()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAssessment = new AssessmentDto
            {
                Id = assessmentId,
                EmployeeId = employeeId,
                Title = "Daily Performance Report",
                Description = "Analysis of daily work performance"
            };

            _fakeAssessmentRepository.Setup(x => x.GetAssessmentById(assessmentId)).ReturnsAsync(expectedAssessment);

            // Act
            var result = await _assessmentService.GetAssessmentById(assessmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssessment.Id, result.Id);
            Assert.Equal(expectedAssessment.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedAssessment.Title, result.Title);
            Assert.Equal(expectedAssessment.Description, result.Description);
        }

        [Fact]
        public async Task UpdateAssessment_ExistingAssessment_ReturnsUpdatedCount()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var assessmentUpdationDto = new AssessmentUpdationDto
            {
                Title = "Prioritizing Tasks Report",
                Description = "Managing time for optimal performance"
            };

            var expectedAssessment = new Assessment
            {
                Id = assessmentId,
                EmployeeId = employeeId,
                Title = assessmentUpdationDto.Title,
                Description = assessmentUpdationDto.Description
            };

            _fakeMapper.Setup(x => x.Map<Assessment>(assessmentUpdationDto)).Returns(expectedAssessment);
            _fakeAssessmentRepository.Setup(x => x.UpdateAssessment(expectedAssessment)).ReturnsAsync(1);

            // Act
            var result = await _assessmentService.UpdateAssessment(assessmentId, assessmentUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAssessment_ExistingAssessment_ReturnsDeletionCount()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();

            _fakeAssessmentRepository.Setup(x => x.DeleteAssessment(assessmentId)).ReturnsAsync(1);

            // Act
            var result = await _assessmentService.DeleteAssessment(assessmentId);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAssessmentItemsById_ExistingAssessmentWithItems_ReturnsAssessmentItemDto()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();
            var choiceId = It.IsAny<int>();

            var assessmentItemDto = new AssessmentItemDto
            {
                Id = assessmentId,
                EmployeeId = employeeId,
                Title = "Daily Performance Report",
                Description = "Analysis of daily work performance",
                Items = new List<ItemDto>
                {
                    new ItemDto
                    {
                        Id = itemId,
                        Question = "What can you say about the overall performance of your work?",
                        QuestionType = "Short Answer",
                        Weight = 50,
                        Target = 25,
                        Required = true,
                        Choices = new List<ChoiceDto>
                        {
                            new ChoiceDto
                            {
                                Id = choiceId,
                                ChoiceValue = "My performance was great.",
                                Weight = 25
                            },
                        },
                        AssessmentId = assessmentId,
                    },
                    new ItemDto
                    {
                        Id = itemId,
                        Question = "What steps are you planning on taking to further improve your job performance before your next review?",
                        QuestionType = "Paragraph",
                        Weight = 100,
                        Target = 50,
                        Required = false,
                        Choices = new List<ChoiceDto>
                        {
                            new ChoiceDto
                            {
                                Id = choiceId,
                                ChoiceValue = "Being able to put effort in every task I do.",
                                Weight = 25
                            }
                        },
                        AssessmentId = assessmentId,
                    },
                },
            };

            _fakeAssessmentRepository.Setup(x => x.GetAssessmentItemsById(assessmentId)).ReturnsAsync(assessmentItemDto);

            // Act
            var result = await _assessmentService.GetAssessmentItemsById(assessmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(assessmentId, result.Id);

            Assert.NotEmpty(result.Items);
            foreach (var item in result.Items)
            {
                Assert.NotNull(item.Id);
                Assert.NotNull(item.Question);
                Assert.NotNull(item.QuestionType);
                Assert.NotNull(item.Weight);
                Assert.NotNull(item.Target);
                Assert.NotNull(item.Required);

                // If items have choices, check them
                if (item.Choices != null && item.Choices.Count > 0)
                {
                    Assert.NotEmpty(item.Choices);
                    foreach (var choice in item.Choices)
                    {
                        Assert.NotNull(choice.Id);
                        Assert.NotNull(choice.ChoiceValue);
                        Assert.NotNull(choice.Weight);
                    }
                }
            }
        }

        [Fact]
        public async Task GetAssessmentsByEmployeeId_ExistingAssessments_ReturnsAssessments()
        {
            // Arrange
            var assessmentId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();

            var expectedAssessments = new List<AssessmentDto>
            {
                new AssessmentDto
                {
                    Id = assessmentId,
                    EmployeeId = employeeId,
                    Title = "Daily Performance Report",
                    Description = "Analysis of daily work performance"
                },
                new AssessmentDto
                {
                    Id = assessmentId,
                    EmployeeId = employeeId,
                    Title = "Quarterly Performance Review",
                    Description = "Assessment progress report for Q3 2023"
                },
            };

            _fakeAssessmentRepository.Setup(x => x.GetAssessmentsByEmployeeId(employeeId)).ReturnsAsync(expectedAssessments);

            // Act
            var result = await _assessmentService.GetAssessmentsByEmployeeId(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssessments.Count, result.Count());
        }
    }
}