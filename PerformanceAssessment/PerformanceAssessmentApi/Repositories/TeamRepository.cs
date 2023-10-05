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
            var sql = "INSERT INTO [dbo].[Team] ([Organization], [FirstName], [LastName], [BusinessType], [BusinessAddress], [TeamCode], [DateTimeCreated], [DateTimeUpdated]) " +
                      "VALUES (@Organization, @FirstName, @LastName, @BusinessType, @BusinessAddress, NEWID(), @DateTimeCreated, @DateTimeUpdated); " +
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

        public async Task<int> UpdateTeam(Team team)
        {
            var sql = "UPDATE [dbo].[Team] SET " +
                      "[Organization] = @Organization, " +
                      "[FirstName] = @FirstName, " +
                      "[LastName] = @LastName, " +
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
                        FirstName = team.FirstName,
                        LastName = team.LastName,
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
    }
}