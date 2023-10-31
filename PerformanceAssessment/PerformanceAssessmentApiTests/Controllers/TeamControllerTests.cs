using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceAssessmentApi.Controllers;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Controllers
{
    public class TeamControllerTests
    {
        private readonly TeamController _controller;
        private readonly Mock<ITeamService> _fakeTeamService;
        private readonly Mock<ILogger<TeamController>> _fakeLogger;

        public TeamControllerTests()
        {
            _fakeTeamService = new Mock<ITeamService>();
            _fakeLogger = new Mock<ILogger<TeamController>>();
            _controller = new TeamController(_fakeTeamService.Object, _fakeLogger.Object);
        }

        [Fact]
        public async Task CreateTeam_ValidTeam_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var teamDto = new TeamCreationDto
            {
                Organization = "Acme Inc.",
                BusinessType = "Technology",
                BusinessAddress = "123 Main Street"
            };

            _fakeTeamService.Setup(service => service.CreateTeam(teamDto))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.CreateTeam(teamDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateTeam_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamDto = new TeamCreationDto
            {
                Organization = "Acme Inc.",
                BusinessType = "Technology",
                BusinessAddress = "123 Main Street"
            };

            _fakeTeamService.Setup(service => service.CreateTeam(teamDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateTeam(teamDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllTeams_ExistingTeams_ReturnsOkObjectResult()
        {
            // Arrange
            var teams = new List<TeamDto>
            {
                new TeamDto(),
                new TeamDto()
            };

            _fakeTeamService.Setup(service => service.GetAllTeams())
                .ReturnsAsync(teams);

            // Act
            var result = await _controller.GetAllTeams();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllTeams_HasNoTeams_ReturnsNoContentResult()
        {
            // Arrange
            _fakeTeamService.Setup(service => service.GetAllTeams())
                .ReturnsAsync(new List<TeamDto>());

            // Act
            var result = await _controller.GetAllTeams();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllTeams_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeTeamService.Setup(service => service.GetAllTeams())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllTeams();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetTeamById_ExistingTeam_ReturnsOkObjectResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var teamDto = new TeamDto { Id = teamId };

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync(teamDto);

            // Act
            var result = await _controller.GetTeamById(teamId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetTeamById_MissingTeam_ReturnsNotFoundResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync((TeamDto)null!);

            // Act
            var result = await _controller.GetTeamById(teamId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetTeamById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetTeamById(teamId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetTeamByCode_ExistingTeam_ReturnsOkObjectResult()
        {
            // Arrange
            var teamCode = Guid.NewGuid();
            var teamDto = new TeamDto { TeamCode = teamCode };

            _fakeTeamService.Setup(service => service.GetTeamByCode(teamCode))
                .ReturnsAsync(teamDto);

            // Act
            var result = await _controller.GetTeamByCode(teamCode);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetTeamByCode_MissingTeam_ReturnsNotFoundResult()
        {
            // Arrange
            var teamCode = Guid.NewGuid();

            _fakeTeamService.Setup(service => service.GetTeamByCode(teamCode))
                .ReturnsAsync((TeamDto)null!);

            // Act
            var result = await _controller.GetTeamByCode(teamCode);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetTeamByCode_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamCode = Guid.NewGuid();

            _fakeTeamService.Setup(service => service.GetTeamByCode(teamCode))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetTeamByCode(teamCode);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateTeam_ExistingTeam_ReturnsOkObjectResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var teamDto = new TeamUpdationDto
            {
                Organization = "XYZ Corporation",
                BusinessType = "Finance",
                BusinessAddress = "456 Elm Avenue"
            };

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync(new TeamDto());

            _fakeTeamService.Setup(service => service.UpdateTeam(teamId, teamDto))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.UpdateTeam(teamId, teamDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateTeam_MissingTeam_ReturnsNotFoundResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var teamDto = new TeamUpdationDto
            {
                Organization = "XYZ Corporation",
                BusinessType = "Finance",
                BusinessAddress = "456 Elm Avenue"
            };

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync((TeamDto)null!);

            // Act
            var result = await _controller.UpdateTeam(teamId, teamDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateTeam_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamId = It.IsAny<int>();
            var teamDto = new TeamUpdationDto
            {
                Organization = "XYZ Corporation",
                BusinessType = "Finance",
                BusinessAddress = "456 Elm Avenue"
            };

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync(new TeamDto());

            _fakeTeamService.Setup(service => service.UpdateTeam(teamId, teamDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.UpdateTeam(teamId, teamDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteTeam_ExistingTeam_ReturnsOkObjectResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync(new TeamDto());

            _fakeTeamService.Setup(service => service.DeleteTeam(teamId))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteTeam(teamId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteTeam_MissingTeam_ReturnsNotFoundResult()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync((TeamDto)null!);

            // Act
            var result = await _controller.DeleteTeam(teamId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteTeam_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var teamId = It.IsAny<int>();

            _fakeTeamService.Setup(service => service.GetTeamById(teamId))
                .ReturnsAsync(new TeamDto());

            _fakeTeamService.Setup(service => service.DeleteTeam(teamId))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteTeam(teamId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}