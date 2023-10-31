using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using PerformanceAssessmentApi.Controllers;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace PerformanceAssessmentApiTests.Controllers
{
    public class TokenControllerTests
    {
        private readonly TokenController _controller;
        private readonly Mock<ITokenService> _fakeTokenService;
        private readonly Mock<IEmployeeService> _fakeEmployeeService;
        private readonly Mock<ILogger<EmployeeController>> _fakeLogger;

        public TokenControllerTests()
        {
            _fakeTokenService = new Mock<ITokenService>();
            _fakeEmployeeService = new Mock<IEmployeeService>();
            _fakeLogger = new Mock<ILogger<EmployeeController>>();
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("The top secret key of the app is performance assessment which is all about implementing evaluations for all employees and admins.");

            _controller = new TokenController(_fakeLogger.Object, _fakeTokenService.Object, _fakeEmployeeService.Object);
        }

        //[Fact]
        //public async Task CreateToken_ValidEmployee_ReturnsOkObjectResult()
        //{
        //    // Arrange
        //    var employeeDetails = new EmployeeInfoDto
        //    {
        //        UserId = It.IsAny<int>(),
        //        TeamId = It.IsAny<int>()
        //    };
        //    var tokenDto = new TokenDto
        //    {
        //        AccessToken = "YourAccessToken",
        //        RefreshToken = "YourRefreshToken"
        //    };

        //    _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamId(employeeDetails.UserId, employeeDetails.TeamId))
        //        .ReturnsAsync(new Employee());
        //    _fakeTokenService.Setup(service => service.CreateToken(It.IsAny<Employee>()))
        //        .Returns("YourAccessToken");
        //    _fakeTokenService.Setup(service => service.GenerateRefreshToken())
        //        .Returns(new RefreshToken { Token = "YourRefreshToken" });

        //    // Act
        //    var result = await _controller.CreateToken(employeeDetails);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        //    var resultValue = Assert.IsType<TokenDto>(okResult.Value);
        //    Assert.Equal(tokenDto.AccessToken, resultValue.AccessToken);
        //    Assert.Equal(tokenDto.RefreshToken, resultValue.RefreshToken);
        //}

        [Fact]
        public async Task CreateToken_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var employeeDetails = new EmployeeInfoDto
            {
                UserId = It.IsAny<int>(),
                TeamId = It.IsAny<int>()
            };

            _fakeEmployeeService.Setup(service => service.GetEmployeeByUserIdAndTeamId(employeeDetails.UserId, employeeDetails.TeamId))
                .ReturnsAsync((Employee)null!);

            // Act
            var result = await _controller.CreateToken(employeeDetails);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorResult.StatusCode);
        }

        //[Fact]
        //public void RefreshToken_ValidToken_ReturnsOkObjectResult()
        //{
        //    // Arrange
        //    var request = new RenewTokenDto
        //    {
        //        RefreshToken = "YourValidRefreshToken"
        //    };
        //    var tokenDto = new TokenDto
        //    {
        //        AccessToken = "YourNewAccessToken",
        //        RefreshToken = "YourNewRefreshToken"
        //    };

        //    _fakeTokenService.Setup(service => service.VerifyToken(request.RefreshToken, It.IsAny<Employee>()))
        //        .Returns("Valid Token");
        //    _fakeTokenService.Setup(service => service.CreateToken(It.IsAny<Employee>()))
        //        .Returns("YourNewAccessToken");
        //    _fakeTokenService.Setup(service => service.GenerateRefreshToken())
        //        .Returns(new RefreshToken { Token = "YourNewRefreshToken" });

        //    // Act
        //    var result = _controller.RefreshToken(request);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        //    var resultValue = Assert.IsType<TokenDto>(okResult.Value);
        //    Assert.Equal(tokenDto.AccessToken, resultValue.AccessToken);
        //    Assert.Equal(tokenDto.RefreshToken, resultValue.RefreshToken);
        //}

        [Fact]
        public void RefreshToken_InvalidToken_ReturnsUnauthorizedObjectResult()
        {
            // Arrange
            var request = new RenewTokenDto
            {
                RefreshToken = "YourInvalidRefreshToken"
            };

            _fakeTokenService.Setup(service => service.VerifyToken(request.RefreshToken, It.IsAny<Employee>()))
                .Returns("Invalid Token");

            // Act
            var result = _controller.RefreshToken(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void RefreshToken_ExpiredToken_ReturnsUnauthorizedObjectResult()
        {
            // Arrange
            var request = new RenewTokenDto
            {
                RefreshToken = "YourExpiredRefreshToken"
            };

            _fakeTokenService.Setup(service => service.VerifyToken(request.RefreshToken, It.IsAny<Employee>()))
                .Returns("Expired Token");

            // Act
            var result = _controller.RefreshToken(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
        }
    }
}