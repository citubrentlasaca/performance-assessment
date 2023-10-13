using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface ITeamService
    {
        Task<Guid> CreateTeam(TeamCreationDto team);

        Task<IEnumerable<TeamDto>> GetAllTeams();

        Task<TeamDto> GetTeamById(int id);

        Task<TeamDto> GetTeamByCode(Guid teamCode);

        Task<int> UpdateTeam(int id, TeamUpdationDto team);

        Task<int> DeleteTeam(int id);

        Task<IEnumerable<TeamDto>> GetTeamsByUserId(int userId);
    }
}