using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class TeamMappingTests
    {
        private readonly IMapper _mapper;

        public TeamMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new TeamMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidTeamCreationDto_ReturnsTeam()
        {
            // Arrange
            var teamCreationDto = new TeamCreationDto
            {
                Organization = "Acme Inc.",
                BusinessType = "Technology",
                BusinessAddress = "123 Main St.",
            };

            // Act
            var team = _mapper.Map<Team>(teamCreationDto);

            // Assert
            Assert.Equal(teamCreationDto.Organization, team.Organization);
            Assert.Equal(teamCreationDto.BusinessType, team.BusinessType);
            Assert.Equal(teamCreationDto.BusinessAddress, team.BusinessAddress);
            Assert.Equal(default(Guid), team.TeamCode);
            Assert.NotNull(team.DateTimeCreated);
            Assert.NotNull(team.DateTimeUpdated);
        }

        [Fact]
        public void Map_ValidTeamUpdationDto_ReturnsTeam()
        {
            // Arrange
            var teamUpdationDto = new TeamUpdationDto
            {
                Organization = "XYZ Corporation",
                BusinessType = "Finance",
                BusinessAddress = "456 Elm St.",
            };

            // Act
            var team = _mapper.Map<Team>(teamUpdationDto);

            // Assert
            Assert.Equal(teamUpdationDto.Organization, team.Organization);
            Assert.Equal(teamUpdationDto.BusinessType, team.BusinessType);
            Assert.Equal(teamUpdationDto.BusinessAddress, team.BusinessAddress);
            Assert.Equal(default(Guid), team.TeamCode);
            Assert.NotNull(team.DateTimeUpdated);
        }
    }
}