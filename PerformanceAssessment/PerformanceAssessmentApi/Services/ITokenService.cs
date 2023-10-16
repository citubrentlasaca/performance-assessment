using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface ITokenService
    {
        string CreateToken(Employee employee);
        RefreshToken GenerateRefreshToken();
        string VerifyToken(string refreshToken, Employee employee);
    }
}