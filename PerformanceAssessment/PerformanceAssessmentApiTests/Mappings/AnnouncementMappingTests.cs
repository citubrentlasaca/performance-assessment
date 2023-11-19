using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class AnnouncementMappingTests
    {
        private readonly IMapper _mapper;

        public AnnouncementMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AnnouncementMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidAnnouncementCreationDto_ReturnsAnnouncement()
        {
            // Arrange
            var announcementCreationDto = new AnnouncementCreationDto
            {
                TeamId = 1,
                Content = "Dear users, we will be performing scheduled maintenance on our system on October 31, 2023. During this time, the system will be temporarily unavailable. We apologize for any inconvenience this may cause and appreciate your understanding. Thank you for being a valued part of our community!",
            };

            // Act
            var announcement = _mapper.Map<Announcement>(announcementCreationDto);

            // Assert
            Assert.Equal(announcementCreationDto.TeamId, announcement.TeamId);
            Assert.Equal(announcementCreationDto.Content, announcement.Content);
            Assert.NotNull(announcement.DateTimeCreated);
            Assert.NotNull(announcement.DateTimeUpdated);
        }

        [Fact]
        public void Map_ValidAnnouncementUpdationDto_ReturnsAnnouncement()
        {
            // Arrange
            var announcementUpdationDto = new AnnouncementUpdationDto
            {
                Content = "Dear team members, it's that time of the year again! We are approaching the end of the quarter, and it's time for our quarterly performance assessments. Your feedback and self-assessment are vital in helping us measure progress and set new goals. Please be prepared to discuss your achievements, challenges, and objectives with your respective managers. We look forward to a productive assessment process.",
            };

            // Act
            var announcement = _mapper.Map<Announcement>(announcementUpdationDto);

            // Assert
            Assert.Equal(announcementUpdationDto.Content, announcement.Content);
            Assert.Null(announcement.DateTimeCreated);
            Assert.NotNull(announcement.DateTimeUpdated);
        }
    }
}