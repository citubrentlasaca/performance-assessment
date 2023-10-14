using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
        string VerifyToken(string refreshToken, User user);
    }
}