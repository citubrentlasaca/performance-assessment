using Microsoft.Extensions.Configuration;
using Moq;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class ITokenServiceTests
    {
        private readonly Mock<IConfiguration> _fakeConfiguration;
        private readonly ITokenService _tokenService;

        public ITokenServiceTests()
        {
            _fakeConfiguration = new Mock<IConfiguration>();
            _tokenService = new TokenService(_fakeConfiguration.Object);
        }

        [Fact]
        public void CreateToken_ValidEmployee_ReturnsJwtToken()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var employee = new Employee
            {
                UserId = userId,
                Role = "Admin"
            };

            _fakeConfiguration.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("The top secret key of the app is performance assessment which is all about implementing evaluations for all employees and admins.");

            // Act
            var result = _tokenService.CreateToken(employee);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateJwtToken_ValidUserId_ReturnsJwtToken()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeConfiguration.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("The top secret key of the app is performance assessment which is all about implementing evaluations for all employees and admins.");

            // Act
            var result = _tokenService.GenerateJwtToken(userId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateRefreshToken_ReturnsRefreshToken()
        {
            // Act
            var result = _tokenService.GenerateRefreshToken();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.NotEmpty(result.Token);
        }

        [Fact]
        public void VerifyToken_ValidToken_ReturnsValidToken()
        {
            // Arrange
            var refreshToken = "validToken";

            var employee = new Employee
            {
                RefreshToken = "validToken",
                TokenExpires = DateTime.UtcNow.AddDays(7)
            };

            // Act
            var result = _tokenService.VerifyToken(refreshToken, employee);

            // Assert
            Assert.Equal("Valid Token", result);
        }

        [Fact]
        public void VerifyToken_InvalidToken_ReturnsInvalidToken()
        {
            // Arrange
            var refreshToken = "invalidToken";

            var employee = new Employee
            {
                RefreshToken = "validToken",
                TokenExpires = DateTime.UtcNow.AddHours(1)
            };

            // Act
            var result = _tokenService.VerifyToken(refreshToken, employee);

            // Assert
            Assert.Equal("Invalid Token", result);
        }

        [Fact]
        public void VerifyToken_ExpiredToken_ReturnsExpiredToken()
        {
            // Arrange
            var refreshToken = "validToken";

            var employee = new Employee
            {
                RefreshToken = "validToken",
                TokenExpires = DateTime.UtcNow.AddMinutes(-1)
            };

            // Act
            var result = _tokenService.VerifyToken(refreshToken, employee);

            // Assert
            Assert.Equal("Expired Token", result);
        }
    }
}