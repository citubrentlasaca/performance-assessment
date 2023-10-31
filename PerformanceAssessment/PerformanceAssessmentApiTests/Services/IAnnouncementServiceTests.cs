using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IAnnouncementServiceTests
    {
        private readonly Mock<IAnnouncementRepository> _fakeAnnouncementRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IAnnouncementService _announcementService;

        public IAnnouncementServiceTests()
        {
            _fakeAnnouncementRepository = new Mock<IAnnouncementRepository>();
            _fakeMapper = new Mock<IMapper>();
            _announcementService = new AnnouncementService(_fakeAnnouncementRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateAnnouncement_ValidData_ReturnsCreatedAnnouncement()
        {
            // Arrange
            var announcementId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var announcementCreationDto = new AnnouncementCreationDto
            {
                TeamId = teamId,
                Content = "Dear users, we will be performing scheduled maintenance on our system on October 17, 2023. During this time, the system will be temporarily unavailable. We apologize for any inconvenience this may cause and appreciate your understanding. Thank you for being a valued part of our community!"
            };

            var expectedAnnouncement = new Announcement
            {
                Id = announcementId,
                TeamId = announcementCreationDto.TeamId,
                Content = announcementCreationDto.Content,
            };

            _fakeMapper.Setup(x => x.Map<Announcement>(announcementCreationDto)).Returns(expectedAnnouncement);
            _fakeAnnouncementRepository.Setup(x => x.CreateAnnouncement(expectedAnnouncement)).ReturnsAsync(announcementId);

            // Act
            var result = await _announcementService.CreateAnnouncement(announcementCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(announcementId, result.Id);
            Assert.Equal(announcementCreationDto.TeamId, result.TeamId);
            Assert.Equal(announcementCreationDto.Content, result.Content);
        }

        [Fact]
        public async Task GetAllAnnouncements_ExistingAnnouncements_ReturnsAnnouncements()
        {
            // Arrange
            var announcementId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var expectedAnnouncements = new List<AnnouncementDto>
            {
                new AnnouncementDto
                {
                    Id = announcementId,
                    TeamId = teamId,
                    Content = "Dear users, we will be performing scheduled maintenance on our system on October 17, 2023. During this time, the system will be temporarily unavailable. We apologize for any inconvenience this may cause and appreciate your understanding. Thank you for being a valued part of our community!",
                },
                new AnnouncementDto
                {
                    Id = announcementId,
                    TeamId = teamId,
                    Content = "Dear team members, it's that time of the year again! We are approaching the end of the quarter, and it's time for our quarterly performance assessments. Your feedback and self-assessment are vital in helping us measure progress and set new goals. Please be prepared to discuss your achievements, challenges, and objectives with your respective managers. We look forward to a productive assessment process.",
                },
            };

            _fakeAnnouncementRepository.Setup(x => x.GetAllAnnouncements()).ReturnsAsync(expectedAnnouncements);

            // Act
            var result = await _announcementService.GetAllAnnouncements();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAnnouncements.Count, result.Count());
            var firstAnnouncement = expectedAnnouncements[0];
            var resultAnnouncement = result.First();
            Assert.Equal(firstAnnouncement.Id, resultAnnouncement.Id);
            Assert.Equal(firstAnnouncement.TeamId, resultAnnouncement.TeamId);
            Assert.Equal(firstAnnouncement.Content, resultAnnouncement.Content);
        }

        [Fact]
        public async Task GetAnnouncementById_ExistingAnnouncement_ReturnsAnnouncement()
        {
            // Arrange
            var announcementId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var expectedAnnouncement = new AnnouncementDto
            {
                Id = announcementId,
                TeamId = teamId,
                Content = "Dear users, we will be performing scheduled maintenance on our system on October 17, 2023. During this time, the system will be temporarily unavailable. We apologize for any inconvenience this may cause and appreciate your understanding. Thank you for being a valued part of our community!",
            };

            _fakeAnnouncementRepository.Setup(x => x.GetAnnouncementById(announcementId)).ReturnsAsync(expectedAnnouncement);

            // Act
            var result = await _announcementService.GetAnnouncementById(announcementId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAnnouncement.Id, result.Id);
            Assert.Equal(expectedAnnouncement.TeamId, result.TeamId);
            Assert.Equal(expectedAnnouncement.Content, result.Content);
        }

        [Fact]
        public async Task UpdateAnnouncement_ExistingAnnouncement_ReturnsUpdatedCount()
        {
            // Arrange
            var announcementId = It.IsAny<int>();
            var teamId = It.IsAny<int>();

            var announcementUpdationDto = new AnnouncementUpdationDto
            {
                Content = "Dear team members, it's that time of the year again! We are approaching the end of the quarter, and it's time for our quarterly performance assessments. Your feedback and self-assessment are vital in helping us measure progress and set new goals. Please be prepared to discuss your achievements, challenges, and objectives with your respective managers. We look forward to a productive assessment process.",
            };

            var expectedAnnouncement = new Announcement
            {
                Id = announcementId,
                TeamId = teamId,
                Content = announcementUpdationDto.Content,
            };

            _fakeMapper.Setup(x => x.Map<Announcement>(announcementUpdationDto)).Returns(expectedAnnouncement);
            _fakeAnnouncementRepository.Setup(x => x.UpdateAnnouncement(expectedAnnouncement)).ReturnsAsync(1);

            // Act
            var result = await _announcementService.UpdateAnnouncement(announcementId, announcementUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAnnouncement_ExistingAnnouncement_ReturnsDeletionCount()
        {
            // Arrange
            var announcementId = It.IsAny<int>();

            _fakeAnnouncementRepository.Setup(x => x.DeleteAnnouncement(announcementId)).ReturnsAsync(1);

            // Act
            var result = await _announcementService.DeleteAnnouncement(announcementId);

            // Assert
            Assert.Equal(1, result);
        }
    }
}