using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using System.Data;

namespace PerformanceAssessmentApi.Repositories
{
    public class SelfAssessmentRepository : ISelfAssessmentRepository
    {
        private readonly DapperContext _context;

        public SelfAssessmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSelfAssessment(SelfAssessment assessment)
        {
            var sql = "INSERT INTO [dbo].[SelfAssessment] ([Title], [Description]) " +
                      "VALUES (@Title, @Description); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, assessment);
            }
        }

        public async Task<IEnumerable<SelfAssessmentDto>> GetAllSelfAssessments()
        {
            var sql = "SELECT * FROM [dbo].[SelfAssessment];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<SelfAssessmentDto>(sql);
            }
        }

        public async Task<SelfAssessmentDto> GetSelfAssessmentById(int id)
        {
            var sql = "SELECT * FROM [dbo].[SelfAssessment] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<SelfAssessmentDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateSelfAssessment(SelfAssessment assessment)
        {
            var sql = "UPDATE [dbo].[SelfAssessment] SET " +
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

        public async Task<int> DeleteSelfAssessment(int id)
        {
            var sql = "DELETE FROM [dbo].[SelfAssessment] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }

        public async Task<SelfAssessmentItemsDto?> GetSelfAssessmentItemsById(int id)
        {
            var assessmentSql = "SELECT * FROM [dbo].[SelfAssessment] WHERE [Id] = @Id;";
            var itemsSql = "SELECT * FROM [dbo].[SelfAssessmentItem] WHERE [SelfAssessmentId] = @Id;";
            var choicesSql = "SELECT * FROM [dbo].[SelfAssessmentChoice] WHERE [SelfAssessmentItemId] IN (SELECT [Id] FROM [dbo].[SelfAssessmentItem] WHERE [SelfAssessmentId] = @Id);";

            using (var con = _context.CreateConnection())
            {
                using (var multi = await con.QueryMultipleAsync($"{assessmentSql}{itemsSql}{choicesSql}", new { Id = id }))
                {
                    var assessment = await multi.ReadSingleOrDefaultAsync<SelfAssessmentItemsDto>();

                    if (assessment != null)
                    {
                        assessment.SelfAssessmentItems = (await multi.ReadAsync<SelfAssessmentItemDto>()).ToList();
                        var choices = (await multi.ReadAsync<SelfAssessmentChoiceDto>()).ToList();

                        foreach (var item in assessment.SelfAssessmentItems)
                        {
                            item.SelfAssessmentChoices = choices.Where(choice => choice.SelfAssessmentItemId == item.Id).ToList();
                        }
                    }
                    return assessment;
                }
            }
        }
    }
}