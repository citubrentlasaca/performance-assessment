using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class EmployerAssessmentItemRepository : IEmployerAssessmentItemRepository
    {
        private readonly DapperContext _context;

        public EmployerAssessmentItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployerAssessmentItem(EmployerAssessmentItem item)
        {
            var sql = "INSERT INTO [dbo].[EmployerAssessmentItem] ([Question], [QuestionType], [Weight], [Target], [Required], [EmployerAssessmentId]) " +
                      "VALUES (@Question, @QuestionType, @Weight, @Target, @Required, @EmployerAssessmentId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, item);
            }
        }

        public async Task<IEnumerable<EmployerAssessmentItemChoiceDto>> GetAllEmployerAssessmentItems()
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Target,
                        i.Required,
                        i.EmployerAssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.EmployerAssessmentItemId AS EmployerAssessmentItemId
                    FROM 
                        [dbo].[EmployerAssessmentItem] AS i
                        LEFT JOIN [dbo].[EmployerAssessmentChoice] AS c ON i.Id = c.EmployerAssessmentItemId;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, EmployerAssessmentItemChoiceDto>();

                var items = await connection.QueryAsync<EmployerAssessmentItemChoiceDto, EmployerAssessmentChoiceDto, EmployerAssessmentItemChoiceDto>(
                            sql,
                            (item, choice) =>
                            {
                                if (itemDictionary.TryGetValue(item.Id, out var currentItem))
                                {
                                    if (choice != null)
                                    {
                                        currentItem.EmployerAssessmentChoices.Add(choice);
                                    }
                                    return currentItem;
                                }
                                else
                                {
                                    item.EmployerAssessmentChoices = new List<EmployerAssessmentChoiceDto>();
                                    if (choice != null)
                                    {
                                        item.EmployerAssessmentChoices.Add(choice);
                                    }
                                    itemDictionary.Add(item.Id, item);
                                    return item;
                                }
                            },
                            splitOn: "Id"
                        );

                return items.Distinct();
            }
        }

        public async Task<EmployerAssessmentItemChoiceDto> GetEmployerAssessmentItemById(int id)
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Target,
                        i.Required,
                        i.EmployerAssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.EmployerAssessmentItemId AS EmployerAssessmentItemId
                    FROM 
                        [dbo].[EmployerAssessmentItem] AS i
                        LEFT JOIN [dbo].[EmployerAssessmentChoice] AS c ON i.Id = c.EmployerAssessmentItemId
                    WHERE 
                        i.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, EmployerAssessmentItemChoiceDto>();

                var item = await connection.QueryAsync<EmployerAssessmentItemChoiceDto, EmployerAssessmentChoiceDto, EmployerAssessmentItemChoiceDto>(
                    sql,
                    (item, choice) =>
                    {
                        EmployerAssessmentItemChoiceDto currentItem = null;

                        if (item != null && !itemDictionary.TryGetValue(item.Id, out currentItem))
                        {
                            currentItem = item;
                            currentItem.EmployerAssessmentChoices = new List<EmployerAssessmentChoiceDto>();
                            itemDictionary.Add(currentItem.Id, currentItem);
                        }

                        if (choice != null && currentItem != null)
                        {
                            currentItem.EmployerAssessmentChoices.Add(choice);
                        }

                        return currentItem;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return item.FirstOrDefault();
            }
        }

        public async Task<int> UpdateEmployerAssessmentItem(EmployerAssessmentItem item)
        {
            var sql = "UPDATE [dbo].[EmployerAssessmentItem] SET " +
                      "[Question] = @Question, " +
                      "[QuestionType] = @QuestionType, " +
                      "[Weight] = @Weight, " +
                      "[Target] = @Target, " +
                      "[Required] = @Required " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = item.Id,
                        Question = item.Question,
                        QuestionType = item.QuestionType,
                        Weight = item.Weight,
                        Target = item.Target,
                        Required = item.Required
                    }
                );
            }
        }

        public async Task<int> DeleteEmployerAssessmentItem(int id)
        {
            var sql = "DELETE FROM [dbo].[EmployerAssessmentItem] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}