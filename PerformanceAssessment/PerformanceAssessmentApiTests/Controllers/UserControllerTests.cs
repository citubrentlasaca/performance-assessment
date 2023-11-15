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
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _fakeUserService;
        private readonly Mock<ITeamService> _fakeTeamService;
        private readonly Mock<ITokenService> _fakeTokenService;
        private readonly Mock<ILogger<UserController>> _fakeLogger;

        public UserControllerTests()
        {
            _fakeUserService = new Mock<IUserService>();
            _fakeTeamService = new Mock<ITeamService>();
            _fakeTokenService = new Mock<ITokenService>();
            _fakeLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_fakeUserService.Object, _fakeTeamService.Object, _fakeTokenService.Object, _fakeLogger.Object);
        }

        [Fact]
        public async Task CreateUser_ValidUser_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var userDto = new UserCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@email.com",
                Password = "StrongPassword123#!",
            };

            _fakeUserService.Setup(s => s.CreateUser(userDto))
                .ReturnsAsync(new UserDto());

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateUser_UserAlreadyExists_ReturnsConflictObjectResult()
        {
            // Arrange
            var userDto = new UserCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@email.com",
                Password = "StrongPassword123#!",
            };

            _fakeUserService.Setup(s => s.CreateUser(userDto))
                .Throws(new Exception("User with the same email address already exists."));

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            var objectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(StatusCodes.Status409Conflict, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userDto = new UserCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@email.com",
                Password = "StrongPassword123#!",
            };

            _fakeUserService.Setup(s => s.CreateUser(userDto))
                .Throws(new Exception());

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetAllUsers_ExistingUsers_ReturnsOkObjectResult()
        {
            // Arrange
            var userDtos = new List<UserDto>
            {
                new UserDto(),
                new UserDto()
            };
            _fakeUserService.Setup(s => s.GetAllUsers()).ReturnsAsync(userDtos);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllUsers_HasNoUsers_ReturnsNoContentResult()
        {
            // Arrange
            _fakeUserService.Setup(s => s.GetAllUsers()).ReturnsAsync(new List<UserDto>());

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllUsers_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _fakeUserService.Setup(service => service.GetAllUsers())
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetUserById_ExistingUser_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var userDto = new UserDto { Id = userId };
            _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(userDto);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetUserById_MissingUser_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync((UserDto)null!);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetUserById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeUserService.Setup(service => service.GetUserById(userId))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetUserByEmailAddressAndPassword_ValidUser_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var email = "johndoe@email.com";
            var password = "StrongPassword123#!";

            _fakeUserService.Setup(s => s.GetUserByEmailAddressAndPassword(email, password)).ReturnsAsync(new User());
            _fakeTokenService.Setup(s => s.GenerateJwtToken(userId)).Returns("MockedJWTToken");

            // Act
            var result = await _controller.GetUserByEmailAddressAndPassword(email, password);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetUserByEmailAddressAndPassword_MissingUser_ReturnsNotFoundResult()
        {
            // Arrange
            var email = "johndoe@email.com";
            var password = "StrongPassword123#!";

            _fakeUserService.Setup(s => s.GetUserByEmailAddressAndPassword(email, password)).ReturnsAsync((User)null!);

            // Act
            var result = await _controller.GetUserByEmailAddressAndPassword(email, password);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetUserByEmailAddressAndPassword_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var email = "johndoe@email.com";
            var password = "StrongPassword123#!";

            _fakeUserService.Setup(s => s.GetUserByEmailAddressAndPassword(email, password)).Throws(new Exception());

            // Act
            var result = await _controller.GetUserByEmailAddressAndPassword(email, password);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        //[Fact]
        //public async Task UpdateUser_ExistingUser_ReturnsOkObjectResult()
        //{
        //    // Arrange
        //    var userId = It.IsAny<int>();
        //    var userDto = new UserDto { Id = userId };
        //    var updatedUser = new UserUpdationDto
        //    {
        //        FirstName = "Jane",
        //        LastName = "Dawn",
        //        EmailAddress = "janedawn@email.com",
        //        Password = "WeakPassword123456",
        //    };

        //    var formFile = new Mock<IFormFile>();

        //    _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(userDto);
        //    _fakeUserService.Setup(s => s.UpdateUser(userId, updatedUser)).ReturnsAsync(1);

        //    // Act
        //    var result = await _controller.UpdateUser(userId, updatedUser, formFile.Object);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        //}

        [Fact]
        public async Task UpdateUser_MissingUser_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var updatedUser = new UserUpdationDto
            {
                FirstName = "Jane",
                LastName = "Dawn",
                EmailAddress = "janedawn@email.com",
                Password = "WeakPassword123456",
            };

            var formFile = new Mock<IFormFile>();

            _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync((UserDto)null!);

            // Act
            var result = await _controller.UpdateUser(userId, updatedUser, formFile.Object);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateUser_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var userDto = new UserDto { Id = userId };
            var updatedUser = new UserUpdationDto
            {
                FirstName = "Jane",
                LastName = "Dawn",
                EmailAddress = "janedawn@email.com",
                Password = "WeakPassword123456",
            };

            var formFile = new Mock<IFormFile>();

            _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(userDto);
            _fakeUserService.Setup(s => s.UpdateUser(userId, updatedUser)).Throws(new Exception());

            // Act
            var result = await _controller.UpdateUser(userId, updatedUser, formFile.Object);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ExistingUser_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var userDto = new UserDto { Id = userId };

            _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(userDto);
            _fakeUserService.Setup(s => s.DeleteUser(userId)).ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_MissingUser_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync((UserDto)null!);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var userDto = new UserDto { Id = userId };

            _fakeUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(userDto);
            _fakeUserService.Setup(s => s.DeleteUser(userId)).Throws(new Exception());

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetTeamsByUserId_ExistingTeams_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var teamDtos = new List<TeamDto>
            {
                new TeamDto(),
                new TeamDto()
            };
            _fakeTeamService.Setup(s => s.GetTeamsByUserId(userId)).ReturnsAsync(teamDtos);

            // Act
            var result = await _controller.GetTeamsByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTeamDtos = Assert.IsType<List<TeamDto>>(okResult.Value);
            Assert.Equal(teamDtos.Count, returnedTeamDtos.Count);
        }

        [Fact]
        public async Task GetTeamsByUserId_HasNoTeamsForUser_ReturnsNoContentResult()
        {
            // Arrange
            var userId = It.IsAny<int>();
            _fakeTeamService.Setup(s => s.GetTeamsByUserId(userId)).ReturnsAsync(new List<TeamDto>());

            // Act
            var result = await _controller.GetTeamsByUserId(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetTeamsByUserId_ExceptionThrwon_ReturnsInternalServerError()
        {
            // Arrange
            var userId = It.IsAny<int>();
            _fakeTeamService.Setup(s => s.GetTeamsByUserId(userId)).Throws(new Exception());

            // Act
            var result = await _controller.GetTeamsByUserId(userId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        }
    }
}