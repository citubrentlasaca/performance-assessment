using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class SelfAssessmentItemRepository : ISelfAssessmentItemRepository
    {
        private readonly DapperContext _context;

        public SelfAssessmentItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSelfAssessmentItem(SelfAssessmentItem item)
        {
            var sql = "INSERT INTO [dbo].[SelfAssessmentItem] ([Question], [QuestionType], [Weight], [Target], [Required], [SelfAssessmentId]) " +
                      "VALUES (@Question, @QuestionType, @Weight, @Target, @Required, @SelfAssessmentId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, item);
            }
        }

        public async Task<IEnumerable<SelfAssessmentItemChoiceDto>> GetAllSelfAssessmentItems()
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Target,
                        i.Required,
                        i.SelfAssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.SelfAssessmentItemId AS SelfAssessmentItemId
                    FROM 
                        [dbo].[SelfAssessmentItem] AS i
                        LEFT JOIN [dbo].[SelfAssessmentChoice] AS c ON i.Id = c.SelfAssessmentItemId;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, SelfAssessmentItemChoiceDto>();

                var items = await connection.QueryAsync<SelfAssessmentItemChoiceDto, SelfAssessmentChoiceDto, SelfAssessmentItemChoiceDto>(
                            sql,
                            (item, choice) =>
                            {
                                if (itemDictionary.TryGetValue(item.Id, out var currentItem))
                                {
                                    if (choice != null)
                                    {
                                        currentItem.SelfAssessmentChoices.Add(choice);
                                    }
                                    return currentItem;
                                }
                                else
                                {
                                    item.SelfAssessmentChoices = new List<SelfAssessmentChoiceDto>();
                                    if (choice != null)
                                    {
                                        item.SelfAssessmentChoices.Add(choice);
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

        public async Task<SelfAssessmentItemChoiceDto> GetSelfAssessmentItemById(int id)
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Target,
                        i.Required,
                        i.SelfAssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.SelfAssessmentItemId AS SelfAssessmentItemId
                    FROM 
                        [dbo].[SelfAssessmentItem] AS i
                        LEFT JOIN [dbo].[SelfAssessmentChoice] AS c ON i.Id = c.SelfAssessmentItemId
                    WHERE 
                        i.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, SelfAssessmentItemChoiceDto>();

                var item = await connection.QueryAsync<SelfAssessmentItemChoiceDto, SelfAssessmentChoiceDto, SelfAssessmentItemChoiceDto>(
                    sql,
                    (item, choice) =>
                    {
                        SelfAssessmentItemChoiceDto currentItem = null;

                        if (item != null && !itemDictionary.TryGetValue(item.Id, out currentItem))
                        {
                            currentItem = item;
                            currentItem.SelfAssessmentChoices = new List<SelfAssessmentChoiceDto>();
                            itemDictionary.Add(currentItem.Id, currentItem);
                        }

                        if (choice != null && currentItem != null)
                        {
                            currentItem.SelfAssessmentChoices.Add(choice);
                        }

                        return currentItem;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return item.FirstOrDefault();
            }
        }

        public async Task<int> UpdateSelfAssessmentItem(SelfAssessmentItem item)
        {
            var sql = "UPDATE [dbo].[SelfAssessmentItem] SET " +
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

        public async Task<int> DeleteSelfAssessmentItem(int id)
        {
            var sql = "DELETE FROM [dbo].[SelfAssessmentItem] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}