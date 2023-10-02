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
            var sql = "INSERT INTO [dbo].[Answer] ([AssessmentId], [ItemId], [AnswerText], [SelectedChoices], [CounterValue]) " +
                      "VALUES (@AssessmentId, @ItemId, @AnswerText, @SelectedChoices, @CounterValue); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, answer);
            }
        }

        public async Task<IEnumerable<AnswerDto>> GetAnswersByAssessmentId(int assessmentId)
        {
            var sql = "SELECT * FROM [dbo].[Answer] WHERE [AssessmentId] = @AssessmentId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AnswerDto>(sql, new { AssessmentId = assessmentId });
            }
        }

        public async Task<IEnumerable<AnswerDto>> GetAnswersByItemId(int itemId)
        {
            var sql = "SELECT * FROM [dbo].[Answer] WHERE [ItemId] = @ItemId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AnswerDto>(sql, new { ItemId = itemId });
            }
        }

        public async Task<AnswerDto> GetAnswersById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Answer] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<AnswerDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateAnswers(Answer answer)
        {
            var sql = "UPDATE [dbo].[Answer] SET " +
                      "[AssessmentId] = @AssessmentId, " +
                      "[ItemId] = @ItemId, " +
                      "[AnswerText] = @AnswerText, " +
                      "[SelectedChoices] = @SelectedChoices, " +
                      "[CounterValue] = @CounterValue " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = answer.Id,
                        AssessmentId = answer.AssessmentId,
                        ItemId = answer.ItemId,
                        AnswerText = answer.AnswerText,
                        SelectedChoices = answer.SelectedChoices,
                        CounterValue = answer.CounterValue
                    }
                );
            }
        }

        public async Task<int> DeleteAnswers(int id)
        {
            var sql = "DELETE FROM [dbo].[Answer] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}