using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface ITeamRepository
    {
        Task<Guid> CreateTeam(Team team);
        Task<IEnumerable<TeamDto>> GetAllTeams();
        Task<TeamDto> GetTeamById(int id);
        Task<int> UpdateTeam(Team team);
        Task<int> DeleteTeam(int id);
    }
}