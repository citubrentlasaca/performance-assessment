using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateTeam(TeamCreationDto team)
        {
            var model = _mapper.Map<Team>(team);
            var teamCode = await _repository.CreateTeam(model);

            return teamCode;
        }

        public Task<IEnumerable<TeamDto>> GetAllTeams()
        {
            return _repository.GetAllTeams();
        }

        public Task<TeamDto> GetTeamById(int id)
        {
            return _repository.GetTeamById(id);
        }

        public Task<TeamDto> GetTeamByCode(Guid teamCode)
        {
            return _repository.GetTeamByCode(teamCode);
        }

        public async Task<int> UpdateTeam(int id, TeamUpdationDto team)
        {
            var model = _mapper.Map<Team>(team);
            model.Id = id;

            return await _repository.UpdateTeam(model);
        }

        public async Task<int> DeleteTeam(int id)
        {
            return await _repository.DeleteTeam(id);
        }

        public async Task<IEnumerable<TeamDto>> GetTeamsByUserId(int userId)
        {
            return await _repository.GetTeamsByUserId(userId);
        }
    }
}