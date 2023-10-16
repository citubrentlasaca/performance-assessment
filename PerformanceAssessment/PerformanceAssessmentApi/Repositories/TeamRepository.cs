using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DapperContext _context;

        public TeamRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateTeam(Team team)
        {
            var sql = "INSERT INTO [dbo].[Team] ([Organization], [BusinessType], [BusinessAddress], [TeamCode], [DateTimeCreated], [DateTimeUpdated]) " +
                      "VALUES (@Organization, @BusinessType, @BusinessAddress, NEWID(), @DateTimeCreated, @DateTimeUpdated); " +
                      "SELECT [TeamCode] FROM [dbo].[Team] WHERE [Id] = SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<Guid>(sql, team);
            }
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeams()
        {
            var sql = "SELECT * FROM [dbo].[Team];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<TeamDto>(sql);
            }
        }

        public async Task<TeamDto> GetTeamById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Team] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<TeamDto>(sql, new { Id = id });
            }
        }

        public async Task<TeamDto> GetTeamByCode(Guid teamCode)
        {
            var sql = "SELECT * FROM [dbo].[Team] WHERE [TeamCode] = @TeamCode;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<TeamDto>(sql, new { TeamCode = teamCode });
            }
        }

        public async Task<int> UpdateTeam(Team team)
        {
            var sql = "UPDATE [dbo].[Team] SET " +
                      "[Organization] = @Organization, " +
                      "[BusinessType] = @BusinessType, " +
                      "[BusinessAddress] = @BusinessAddress, " +
                      "[DateTimeUpdated] = @DateTimeUpdated " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = team.Id,
                        Organization = team.Organization,
                        BusinessType = team.BusinessType,
                        BusinessAddress = team.BusinessAddress,
                        DateTimeUpdated = team.DateTimeUpdated
                    }
                );
            }
        }

        public async Task<int> DeleteTeam(int id)
        {
            var sql = "DELETE FROM [dbo].[Team] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<TeamDto>> GetTeamsByUserId(int userId)
        {
            var sql = "SELECT t.* FROM [dbo].[Team] t " +
                      "INNER JOIN [dbo].[Employee] e ON t.Id = e.TeamId " +
                      "WHERE e.UserId = @UserId";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<TeamDto>(sql, new { UserId = userId });
            }
        }
    }
}