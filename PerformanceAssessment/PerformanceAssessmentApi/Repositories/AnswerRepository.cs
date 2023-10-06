using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DapperContext _context;

        public AnswerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAnswers(Answer answer)
        {
            var sql = "INSERT INTO [dbo].[Answer] " +
                      "([EmployeeId], [ItemId], [AnswerText], [SelectedChoices], [CounterValue], [IsDeleted], [DateTimeAnswered]) " +
                      "VALUES (@EmployeeId, @ItemId, @AnswerText, @SelectedChoices, @CounterValue, 0, @DateTimeAnswered); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, answer);
            }
        }

        public async Task<IEnumerable<AnswerDto>> GetAnswersByItemId(int itemId)
        {
            var sql = "SELECT * FROM [dbo].[Answer] WHERE [ItemId] = @ItemId AND [IsDeleted] = 0;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AnswerDto>(sql, new { ItemId = itemId });
            }
        }

        public async Task<AnswerDto> GetAnswersById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Answer] WHERE [Id] = @Id AND [IsDeleted] = 0;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<AnswerDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateAnswers(Answer answer)
        {
            var sql = "UPDATE [dbo].[Answer] SET " +
                      "[ItemId] = @ItemId, " +
                      "[AnswerText] = @AnswerText, " +
                      "[SelectedChoices] = @SelectedChoices, " +
                      "[CounterValue] = @CounterValue " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteAsync(sql, answer);
            }
        }

        public async Task<int> DeleteAnswers(int id)
        {
            var sql = "UPDATE [dbo].[Answer] SET [IsDeleted] = 1 WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}