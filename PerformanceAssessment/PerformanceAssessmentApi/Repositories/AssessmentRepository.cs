using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using System.Data;

namespace PerformanceAssessmentApi.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly DapperContext _context;

        public AssessmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAssessment(Assessment assessment)
        {
            var sql = "INSERT INTO [dbo].[Assessment] ([Title], [Description]) " +
                      "VALUES (@Title, @Description); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, assessment);
            }
        }

        public async Task<IEnumerable<AssessmentDto>> GetAllAssessments()
        {
            var sql = "SELECT * FROM [dbo].[Assessment];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AssessmentDto>(sql);
            }
        }

        public async Task<AssessmentDto> GetAssessmentById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Assessment] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<AssessmentDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateAssessment(Assessment assessment)
        {
            var sql = "UPDATE [dbo].[Assessment] SET " +
                      "[Title] = @Title, " +
                      "[Description] = @Description " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = assessment.Id,
                        Title = assessment.Title,
                        Description = assessment.Description
                    }
                );
            }
        }

        public async Task<int> DeleteAssessment(int id)
        {
            var sql = "DELETE FROM [dbo].[Assessment] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }

        public async Task<AssessmentItemDto?> GetAssessmentItemsById(int id)
        {
            var assessmentSql = "SELECT * FROM [dbo].[Assessment] WHERE [Id] = @Id;";
            var itemsSql = "SELECT * FROM [dbo].[Item] WHERE [AssessmentId] = @Id;";
            var choicesSql = "SELECT * FROM [dbo].[Choice] WHERE [ItemId] IN (SELECT [Id] FROM [dbo].[Item] WHERE [AssessmentId] = @Id);";

            using (var con = _context.CreateConnection())
            {
                using (var multi = await con.QueryMultipleAsync($"{assessmentSql}{itemsSql}{choicesSql}", new { Id = id }))
                {
                    var assessment = await multi.ReadSingleOrDefaultAsync<AssessmentItemDto>();

                    if (assessment != null)
                    {
                        assessment.Items = (await multi.ReadAsync<ItemDto>()).ToList();
                        var choices = (await multi.ReadAsync<ChoiceDto>()).ToList();

                        foreach (var item in assessment.Items)
                        {
                            item.Choices = choices.Where(choice => choice.ItemId == item.Id).ToList();
                        }
                    }
                    return assessment;
                }
            }
        }
    }
}