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

        public async Task<AssessmentItemDto> GetAssessmentItemsById(int id)
        {
            var sql = @"
                    SELECT 
                        a.Id AS Id, 
                        a.Title AS Title, 
                        a.Description AS Description, 
                        i.Id AS Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Required,
                        i.AssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.ItemId
                    FROM 
                        [dbo].[Assessment] AS a 
                        LEFT JOIN [dbo].[Item] AS i ON a.Id = i.AssessmentId 
                        LEFT JOIN [dbo].[Choice] AS c ON i.Id = c.ItemId 
                    WHERE 
                        a.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var assessments = await connection.QueryAsync<AssessmentItemDto, ItemDto, ChoiceDto, AssessmentItemDto>(
                    sql,
                    (assessment, item, choice) =>
                    {
                        assessment.Items.Add(item);
                        item.Choices = choice;
                        return assessment;
                    },
                    new { Id = id },
                    splitOn: "Id, Id"
                );

                var result = assessments
                    .GroupBy(assessment => assessment.Id)
                    .Select(assessmentGroup =>
                    {
                        var groupedAssessment = assessmentGroup.First();
                        groupedAssessment.Items = assessmentGroup
                            .Select(item => item.Items.FirstOrDefault())
                            .Where(item => item != null)
                            .GroupBy(item => item.Id)
                            .Select(itemGroup => itemGroup.First())
                            .ToList();
                        return groupedAssessment;
                    })
                    .SingleOrDefault();

                return result;
            }
        }
    }
}