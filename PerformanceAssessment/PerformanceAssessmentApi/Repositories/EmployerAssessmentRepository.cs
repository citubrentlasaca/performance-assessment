using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using System.Data;

namespace PerformanceAssessmentApi.Repositories
{
    public class EmployerAssessmentRepository : IEmployerAssessmentRepository
    {
        private readonly DapperContext _context;

        public EmployerAssessmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployerAssessment(EmployerAssessment assessment)
        {
            var sql = "INSERT INTO [dbo].[EmployerAssessment] ([Title], [Description]) " +
                      "VALUES (@Title, @Description); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, assessment);
            }
        }

        public async Task<IEnumerable<EmployerAssessmentDto>> GetAllEmployerAssessments()
        {
            var sql = "SELECT * FROM [dbo].[EmployerAssessment];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployerAssessmentDto>(sql);
            }
        }

        public async Task<EmployerAssessmentDto> GetEmployerAssessmentById(int id)
        {
            var sql = "SELECT * FROM [dbo].[EmployerAssessment] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployerAssessmentDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateEmployerAssessment(EmployerAssessment assessment)
        {
            var sql = "UPDATE [dbo].[EmployerAssessment] SET " +
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

        public async Task<int> DeleteEmployerAssessment(int id)
        {
            var sql = "DELETE FROM [dbo].[EmployerAssessment] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }

        public async Task<EmployerAssessmentItemsDto?> GetEmployerAssessmentItemsById(int id)
        {
            var assessmentSql = "SELECT * FROM [dbo].[EmployerAssessment] WHERE [Id] = @Id;";
            var itemsSql = "SELECT * FROM [dbo].[EmployerAssessmentItem] WHERE [EmployerAssessmentId] = @Id;";
            var choicesSql = "SELECT * FROM [dbo].[EmployerAssessmentChoice] WHERE [EmployerAssessmentItemId] IN (SELECT [Id] FROM [dbo].[EmployerAssessmentItem] WHERE [EmployerAssessmentId] = @Id);";

            using (var con = _context.CreateConnection())
            {
                using (var multi = await con.QueryMultipleAsync($"{assessmentSql}{itemsSql}{choicesSql}", new { Id = id }))
                {
                    var assessment = await multi.ReadSingleOrDefaultAsync<EmployerAssessmentItemsDto>();

                    if (assessment != null)
                    {
                        assessment.EmployerAssessmentItems = (await multi.ReadAsync<EmployerAssessmentItemDto>()).ToList();
                        var choices = (await multi.ReadAsync<EmployerAssessmentChoiceDto>()).ToList();

                        foreach (var item in assessment.EmployerAssessmentItems)
                        {
                            item.EmployerAssessmentChoices = choices.Where(choice => choice.EmployerAssessmentItemId == item.Id).ToList();
                        }
                    }
                    return assessment;
                }
            }
        }
    }
}