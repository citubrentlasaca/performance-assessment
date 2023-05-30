using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly DapperContext _context;

        public ChoiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateChoice(Choice choice)
        {
            var sql = "INSERT INTO [dbo].[Choice] ([ChoiceValue], [ItemId]) " +
                      "VALUES (@ChoiceValue, @ItemId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, choice);
            }
        }

        public async Task<IEnumerable<ChoiceDto>> GetAllChoices()
        {
            var sql = "SELECT * FROM [dbo].[Choice];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<ChoiceDto>(sql);
            }
        }

        public async Task<ChoiceDto> GetChoiceById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Choice] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<ChoiceDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateChoice(Choice choice)
        {
            var sql = "UPDATE [dbo].[Choice] SET " +
                      "[ChoiceValue] = @ChoiceValue " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = choice.Id,
                        ChoiceValue = choice.ChoiceValue
                    }
                );
            }
        }

        public async Task<int> DeleteChoice(int id)
        {
            var sql = "DELETE FROM [dbo].[Choice] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}