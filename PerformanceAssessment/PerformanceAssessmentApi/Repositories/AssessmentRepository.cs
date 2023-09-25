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
                        c.Weight,
                        c.ItemId AS ItemId
                    FROM 
                        [dbo].[Assessment] AS a 
                        LEFT JOIN [dbo].[Item] AS i ON a.Id = i.AssessmentId 
                        LEFT JOIN [dbo].[Choice] AS c ON i.Id = c.ItemId 
                    WHERE 
                        a.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var assessmentDictionary = new Dictionary<int, AssessmentItemDto>();

                await connection.QueryAsync<AssessmentItemDto, ItemDto, ChoiceDto, AssessmentItemDto>(
                    sql,
                    (assessment, item, choice) =>
                    {
                        if (!assessmentDictionary.TryGetValue(assessment.Id, out var currentAssessment))
                        {
                            currentAssessment = assessment;
                            currentAssessment.Items = new List<ItemDto>();
                            assessmentDictionary.Add(currentAssessment.Id, currentAssessment);
                        }

                        if (currentAssessment.Items.Any(existingItem => existingItem.Id == item.Id))
                        {
                            currentAssessment.Items.Single(existingItem => existingItem.Id == item.Id).Choices.Add(choice);
                        }
                        else
                        {
                            item.Choices = new List<ChoiceDto> { choice };
                            currentAssessment.Items.Add(item);
                        }

                        return assessment;
                    },
                    new { Id = id },
                    splitOn: "Id, Id, Id"
                );

                var result = assessmentDictionary.Values.SingleOrDefault();

                return result;
            }
        }
    }
}