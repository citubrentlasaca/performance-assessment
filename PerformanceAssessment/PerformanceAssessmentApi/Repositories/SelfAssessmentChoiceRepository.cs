using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class SelfAssessmentChoiceRepository : ISelfAssessmentChoiceRepository
    {
        private readonly DapperContext _context;

        public SelfAssessmentChoiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSelfAssessmentChoice(SelfAssessmentChoice choice)
        {
            var sql = "INSERT INTO [dbo].[SelfAssessmentChoice] ([ChoiceValue], [SelfAssessmentItemId]) " +
                      "VALUES (@ChoiceValue, @SelfAssessmentItemId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, choice);
            }
        }

        public async Task<IEnumerable<SelfAssessmentChoiceDto>> GetAllSelfAssessmentChoices()
        {
            var sql = "SELECT * FROM [dbo].[SelfAssessmentChoice];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<SelfAssessmentChoiceDto>(sql);
            }
        }

        public async Task<SelfAssessmentChoiceDto> GetSelfAssessmentChoiceById(int id)
        {
            var sql = "SELECT * FROM [dbo].[SelfAssessmentChoice] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<SelfAssessmentChoiceDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateSelfAssessmentChoice(SelfAssessmentChoice choice)
        {
            var sql = "UPDATE [dbo].[SelfAssessmentChoice] SET " +
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

        public async Task<int> DeleteSelfAssessmentChoice(int id)
        {
            var sql = "DELETE FROM [dbo].[SelfAssessmentChoice] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}