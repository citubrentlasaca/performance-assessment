using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DapperContext _context;

        public ItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateItem(Item item)
        {
            var sql = "INSERT INTO [dbo].[Item] ([Question], [QuestionType], [Weight], [Required], [AssessmentId]) " +
                      "VALUES (@Question, @QuestionType, @Weight, @Required, @AssessmentId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, item);
            }
        }

        public async Task<IEnumerable<ItemChoiceDto>> GetAllItems()
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Required,
                        i.AssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.ItemId AS ItemId
                    FROM 
                        [dbo].[Item] AS i
                        LEFT JOIN [dbo].[Choice] AS c ON i.Id = c.ItemId;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, ItemChoiceDto>();

                var items = await connection.QueryAsync<ItemChoiceDto, ChoiceDto, ItemChoiceDto>(
                            sql,
                            (item, choice) =>
                            {
                                if (itemDictionary.TryGetValue(item.Id, out var currentItem))
                                {
                                    if (choice != null)
                                    {
                                        currentItem.Choices.Add(choice);
                                    }
                                    return currentItem;
                                }
                                else
                                {
                                    item.Choices = new List<ChoiceDto>();
                                    if (choice != null)
                                    {
                                        item.Choices.Add(choice);
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

        public async Task<ItemChoiceDto> GetItemById(int id)
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Required,
                        i.AssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.ItemId AS ItemId
                    FROM 
                        [dbo].[Item] AS i
                        LEFT JOIN [dbo].[Choice] AS c ON i.Id = c.ItemId
                    WHERE 
                        i.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, ItemChoiceDto>();

                var item = await connection.QueryAsync<ItemChoiceDto, ChoiceDto, ItemChoiceDto>(
                    sql,
                    (item, choice) =>
                    {
                        ItemChoiceDto currentItem = null;

                        if (item != null && !itemDictionary.TryGetValue(item.Id, out currentItem))
                        {
                            currentItem = item;
                            currentItem.Choices = new List<ChoiceDto>();
                            itemDictionary.Add(currentItem.Id, currentItem);
                        }

                        if (choice != null && currentItem != null)
                        {
                            currentItem.Choices.Add(choice);
                        }

                        return currentItem;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return item.FirstOrDefault();
            }
        }

        public async Task<int> UpdateItem(Item item)
        {
            var sql = "UPDATE [dbo].[Item] SET " +
                      "[Question] = @Question, " +
                      "[QuestionType] = @QuestionType, " +
                      "[Weight] = @Weight, " +
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
                        Required = item.Required
                    }
                );
            }
        }

        public async Task<int> DeleteItem(int id)
        {
            var sql = "DELETE FROM [dbo].[Item] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}