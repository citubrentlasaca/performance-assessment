using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApiTests.Utils
{
    public class PasswordHasherTests
    {
        [Fact]
        public void HashPassword_WithValidPassword_ReturnsSuccess()
        {
            // Arrange
            var password = "mySecretPassword";

            // Act
            var (hash, salt) = PasswordHasher.HashPassword(password);
            var result = PasswordHasher.VerifyPassword(password, hash, salt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WithValidPasswordHashAndSalt_ReturnsSuccess()
        {
            // Arrange
            var password = "mySecretPassword";
            var (hash, salt) = PasswordHasher.HashPassword(password);

            // Act
            var result = PasswordHasher.VerifyPassword(password, hash, salt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WithIncorrectPassword_ReturnsFailure()
        {
            // Arrange
            var password = "mySecretPassword";
            var (hash, salt) = PasswordHasher.HashPassword(password);
            var incorrectPassword = "incorrectPassword";

            // Act
            var result = PasswordHasher.VerifyPassword(incorrectPassword, hash, salt);

            // Assert
            Assert.False(result);
        }
    }
}