using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class ITeamServiceTests
    {
        private readonly Mock<ITeamRepository> _fakeRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly ITeamService _teamService;

        public ITeamServiceTests()
        {
            _fakeRepository = new Mock<ITeamRepository>();
            _fakeMapper = new Mock<IMapper>();
            _teamService = new TeamService(_fakeRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateTeam_ValidObjectPassed_ReturnsCreatedTeamCode()
        {
            // Arrange
            var teamCode = Guid.NewGuid();

            var teamCreationDto = new TeamCreationDto
            {
                Organization = "Company A",
                BusinessType = "Type X",
                BusinessAddress = "123 Main St"
            };

            var team = new Team
            {
                Organization = teamCreationDto.Organization,
                BusinessType = teamCreationDto.BusinessType,
                BusinessAddress = teamCreationDto.BusinessAddress,
                TeamCode = teamCode
            };

            _fakeMapper.Setup(x => x.Map<Team>(teamCreationDto)).Returns(team);
            _fakeRepository.Setup(x => x.CreateTeam(team)).ReturnsAsync(teamCode);

            // Act
            var result = await _teamService.CreateTeam(teamCreationDto);

            // Assert
            Assert.Equal(teamCode, result);
        }

        [Fact]
        public async Task GetAllTeams_ExistingTeams_ReturnsAllTeams()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            var teamDtos = new List<TeamDto>
            {
                new TeamDto
                {
                    Id = teamId,
                    Organization = "Company A",
                    BusinessType = "Type X",
                    BusinessAddress = "123 Main St",
                    TeamCode = Guid.NewGuid()
                },
                new TeamDto
                {
                    Id = teamId,
                    Organization = "Company B",
                    BusinessType = "Type Y",
                    BusinessAddress = "456 Elm St",
                    TeamCode = Guid.NewGuid()
                },
            };

            _fakeRepository.Setup(x => x.GetAllTeams()).ReturnsAsync(teamDtos);

            // Act
            var result = await _teamService.GetAllTeams();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teamDtos.Count, result.Count());
        }

        [Fact]
        public async Task GetTeamById_ExistingTeam_ReturnsTeam()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            var teamDto = new TeamDto
            {
                Id = teamId,
                Organization = "Company A",
                BusinessType = "Type X",
                BusinessAddress = "123 Main St",
                TeamCode = Guid.NewGuid()
            };

            _fakeRepository.Setup(x => x.GetTeamById(teamId)).ReturnsAsync(teamDto);

            // Act
            var result = await _teamService.GetTeamById(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teamDto.Id, result.Id);
            Assert.Equal(teamDto.Organization, result.Organization);
            Assert.Equal(teamDto.BusinessType, result.BusinessType);
            Assert.Equal(teamDto.BusinessAddress, result.BusinessAddress);
            Assert.Equal(teamDto.TeamCode, result.TeamCode);
        }

        [Fact]
        public async Task GetTeamByCode_ExistingTeam_ReturnsTeam()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var teamCode = Guid.NewGuid();

            var teamDto = new TeamDto
            {
                Id = teamId,
                Organization = "Company A",
                BusinessType = "Type X",
                BusinessAddress = "123 Main St",
                TeamCode = teamCode
            };

            _fakeRepository.Setup(x => x.GetTeamByCode(teamCode)).ReturnsAsync(teamDto);

            // Act
            var result = await _teamService.GetTeamByCode(teamCode);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teamDto.Id, result.Id);
            Assert.Equal(teamDto.Organization, result.Organization);
            Assert.Equal(teamDto.BusinessType, result.BusinessType);
            Assert.Equal(teamDto.BusinessAddress, result.BusinessAddress);
            Assert.Equal(teamDto.TeamCode, result.TeamCode);
        }

        [Fact]
        public async Task UpdateTeam_ExistingTeam_ReturnsNumberOfUpdatedTeams()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            var teamUpdationDto = new TeamUpdationDto
            {
                Organization = "Company B",
                BusinessType = "Type Y",
                BusinessAddress = "456 Elm St"
            };

            var team = new Team
            {
                Id = teamId,
                Organization = teamUpdationDto.Organization,
                BusinessType = teamUpdationDto.BusinessType,
                BusinessAddress = teamUpdationDto.BusinessAddress,
            };

            _fakeMapper.Setup(x => x.Map<Team>(teamUpdationDto)).Returns(team);
            _fakeRepository.Setup(x => x.UpdateTeam(team)).ReturnsAsync(1);

            // Act
            var result = await _teamService.UpdateTeam(teamId, teamUpdationDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteTeam_ExistingTeam_ReturnsNumberOfDeletedTeams()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeRepository.Setup(x => x.DeleteTeam(teamId)).ReturnsAsync(1);

            // Act
            var result = await _teamService.DeleteTeam(teamId);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetTeamsByUserId_ExistingUser_ReturnsTeams()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            var teamDtos = new List<TeamDto>
            {
                new TeamDto
                {
                    Id = teamId,
                    Organization = "Company A",
                    BusinessType = "Type X",
                    BusinessAddress = "123 Main St",
                    TeamCode = Guid.NewGuid()
                },
                new TeamDto
                {
                    Id = teamId,
                    Organization = "Company B",
                    BusinessType = "Type Y",
                    BusinessAddress = "456 Elm St",
                    TeamCode = Guid.NewGuid()
                },
            };

            _fakeRepository.Setup(x => x.GetTeamsByUserId(teamId)).ReturnsAsync(teamDtos);

            // Act
            var result = await _teamService.GetTeamsByUserId(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teamDtos.Count, result.Count());
        }
    }
}