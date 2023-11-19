using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IAnswerServiceTests
    {
        private readonly Mock<IAnswerRepository> _fakeAnswerRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IAnswerService _answerService;

        public IAnswerServiceTests()
        {
            _fakeAnswerRepository = new Mock<IAnswerRepository>();
            _fakeMapper = new Mock<IMapper>();
            _answerService = new AnswerService(_fakeAnswerRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task SaveAnswers_ValidData_ReturnsSavedAnswers()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var answerCreationDto = new AnswerCreationDto
            {
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "Performance Evaluation",
                SelectedChoices = "Efficiency, Punctuality",
                CounterValue = 10
            };

            var expectedAnswer = new Answer
            {
                Id = answerId,
                EmployeeId = answerCreationDto.EmployeeId,
                ItemId = answerCreationDto.ItemId,
                AnswerText = answerCreationDto.AnswerText,
                SelectedChoices = answerCreationDto.SelectedChoices,
                CounterValue = answerCreationDto.CounterValue
            };

            _fakeMapper.Setup(x => x.Map<Answer>(answerCreationDto)).Returns(expectedAnswer);
            _fakeAnswerRepository.Setup(x => x.SaveAnswers(expectedAnswer)).ReturnsAsync(answerId);

            // Act
            var result = await _answerService.SaveAnswers(answerCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(answerId, result.Id);
            Assert.Equal(answerCreationDto.EmployeeId, result.EmployeeId);
            Assert.Equal(answerCreationDto.ItemId, result.ItemId);
            Assert.Equal(answerCreationDto.AnswerText, result.AnswerText);
            Assert.Equal(answerCreationDto.SelectedChoices, result.SelectedChoices);
            Assert.Equal(answerCreationDto.CounterValue, result.CounterValue);
        }

        [Fact]
        public async Task GetAnswersByItemId_ExistingAnswers_ReturnsAnswers()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var expectedAnswers = new List<AnswerDto>
            {
                new AnswerDto
                {
                    Id = answerId,
                    EmployeeId = employeeId,
                    ItemId = itemId,
                    AnswerText = "Work Ethics",
                    SelectedChoices = "Goals, Tasks",
                    CounterValue = 15
                },
                new AnswerDto
                {
                    Id = answerId,
                    EmployeeId = employeeId,
                    ItemId = itemId,
                    AnswerText = "Determination",
                    SelectedChoices = "Skills, Teamwork",
                    CounterValue = 20
                },
            };

            _fakeAnswerRepository.Setup(x => x.GetAnswersByItemId(itemId)).ReturnsAsync(expectedAnswers);

            // Act
            var result = await _answerService.GetAnswersByItemId(itemId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAnswers.Count, result.Count());
            var firstAnswer = expectedAnswers.First();
            var resultAnswer = result.First();
            Assert.Equal(firstAnswer.Id, resultAnswer.Id);
            Assert.Equal(firstAnswer.EmployeeId, resultAnswer.EmployeeId);
            Assert.Equal(firstAnswer.ItemId, resultAnswer.ItemId);
            Assert.Equal(firstAnswer.AnswerText, resultAnswer.AnswerText);
            Assert.Equal(firstAnswer.SelectedChoices, resultAnswer.SelectedChoices);
            Assert.Equal(firstAnswer.CounterValue, resultAnswer.CounterValue);
        }

        [Fact]
        public async Task GetAnswersById_ExistingAnswers_ReturnsAnswers()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var expectedAnswer = new AnswerDto
            {
                Id = answerId,
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "Efficiency",
                SelectedChoices = "Hard Work, Work Smart",
                CounterValue = 25
            };

            _fakeAnswerRepository.Setup(x => x.GetAnswersById(answerId)).ReturnsAsync(expectedAnswer);

            // Act
            var result = await _answerService.GetAnswersById(answerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAnswer.Id, result.Id);
            Assert.Equal(expectedAnswer.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedAnswer.ItemId, result.ItemId);
            Assert.Equal(expectedAnswer.AnswerText, result.AnswerText);
            Assert.Equal(expectedAnswer.SelectedChoices, result.SelectedChoices);
            Assert.Equal(expectedAnswer.CounterValue, result.CounterValue);
        }

        [Fact]
        public async Task UpdateAnswers_ExistingAnswers_ReturnsUpdatedCount()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var answerUpdationDto = new AnswerUpdationDto
            {
                EmployeeId = employeeId,
                ItemId = itemId,
                AnswerText = "The team was good.",
                SelectedChoices = "Work Environment, Duties",
                CounterValue = 30
            };

            var expectedAnswer = new Answer
            {
                Id = answerId,
                EmployeeId = answerUpdationDto.EmployeeId,
                ItemId = answerUpdationDto.ItemId,
                AnswerText = answerUpdationDto.AnswerText,
                SelectedChoices = answerUpdationDto.SelectedChoices,
                CounterValue = answerUpdationDto.CounterValue
            };

            _fakeMapper.Setup(x => x.Map<Answer>(answerUpdationDto)).Returns(expectedAnswer);
            _fakeAnswerRepository.Setup(x => x.UpdateAnswers(expectedAnswer)).ReturnsAsync(1);

            // Act
            var result = await _answerService.UpdateAnswers(answerId, answerUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAnswers_ExistingAnswers_ReturnsDeletionCount()
        {
            // Arrange
            var answerId = It.IsAny<int>();

            _fakeAnswerRepository.Setup(x => x.DeleteAnswers(answerId)).ReturnsAsync(1);

            // Act
            var result = await _answerService.DeleteAnswers(answerId);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAssessmentAnswersByEmployeeIdAndAssessmentId_ExistingData_ReturnsAssessmentAnswersDto()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var employeeId = It.IsAny<int>();
            var assessmentId = It.IsAny<int>();
            var itemId = It.IsAny<int>();

            var expectedAssessmentAnswersDto = new AssessmentAnswersDto
            {
                Id = assessmentId,
                EmployeeId = employeeId,
                Title = "Daily Performance Report",
                Description = "Analysis of daily work performance",
                Items = new List<ItemAnswersDto>
                {
                    new ItemAnswersDto
                    {
                        Id = itemId,
                        Question = "What can you say about the overall performance of your work?",
                        QuestionType = "Short Answer",
                        Weight = 50,
                        Target = 25,
                        Required = true,
                        Answers = new List<AnswerDto>
                        {
                            new AnswerDto
                            {
                                Id = answerId,
                                EmployeeId = employeeId,
                                ItemId = itemId,
                                AnswerText = "My performance was great.",
                                SelectedChoices = "Efficiency, Teamwork",
                                CounterValue = 10,
                                IsDeleted = false
                            }
                        },
                    },
                    new ItemAnswersDto
                    {
                        Id = itemId,
                        Question = "What steps are you planning on taking to further improve your job performance before your next review?",
                        QuestionType = "Paragraph",
                        Weight = 30,
                        Target = 50,
                        Required = false,
                        Answers = new List<AnswerDto>
                        {
                            new AnswerDto
                            {
                                Id = answerId,
                                EmployeeId = employeeId,
                                ItemId = itemId,
                                AnswerText = "To work hard and meet the company's expectations.",
                                SelectedChoices = "Evaluation, Analytics",
                                CounterValue = 15,
                                IsDeleted = false
                            }
                        },
                    },
                }
            };

            _fakeAnswerRepository.Setup(x => x.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId)).ReturnsAsync(expectedAssessmentAnswersDto);

            // Act
            var result = await _answerService.GetAssessmentAnswersByEmployeeIdAndAssessmentId(employeeId, assessmentId);

            // Assert
            Assert.Equal(expectedAssessmentAnswersDto.Id, result.Id);
            Assert.Equal(expectedAssessmentAnswersDto.EmployeeId, result.EmployeeId);
            Assert.Equal(expectedAssessmentAnswersDto.Title, result.Title);
            Assert.Equal(expectedAssessmentAnswersDto.Description, result.Description);

            // Additional assertions for nested lists
            Assert.NotEmpty(result.Items);
            Assert.Equal(expectedAssessmentAnswersDto.Items.Count, result.Items.Count);

            var firstExpectedItem = expectedAssessmentAnswersDto.Items.First();
            var firstResultItem = result.Items.First();
            Assert.Equal(firstExpectedItem.Id, firstResultItem.Id);
            Assert.Equal(firstExpectedItem.Question, firstResultItem.Question);
            Assert.Equal(firstExpectedItem.QuestionType, firstResultItem.QuestionType);
            Assert.Equal(firstExpectedItem.Weight, firstResultItem.Weight);
            Assert.Equal(firstExpectedItem.Target, firstResultItem.Target);
            Assert.Equal(firstExpectedItem.Required, firstResultItem.Required);

            var firstExpectedAnswer = firstExpectedItem.Answers.First();
            var firstResultAnswer = firstResultItem.Answers.First();
            Assert.Equal(firstExpectedAnswer.Id, firstResultAnswer.Id);
            Assert.Equal(firstExpectedAnswer.EmployeeId, firstResultAnswer.EmployeeId);
            Assert.Equal(firstExpectedAnswer.ItemId, firstResultAnswer.ItemId);
            Assert.Equal(firstExpectedAnswer.AnswerText, firstResultAnswer.AnswerText);
            Assert.Equal(firstExpectedAnswer.SelectedChoices, firstResultAnswer.SelectedChoices);
            Assert.Equal(firstExpectedAnswer.CounterValue, firstResultAnswer.CounterValue);
            Assert.Equal(firstExpectedAnswer.IsDeleted, firstResultAnswer.IsDeleted);
        }
    }
}