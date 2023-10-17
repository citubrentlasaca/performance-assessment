using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface ITokenService
    {
        string CreateToken(Employee employee);
        string GenerateJwtToken(int userId);
        RefreshToken GenerateRefreshToken();
        string VerifyToken(string refreshToken, Employee employee);
    }
}