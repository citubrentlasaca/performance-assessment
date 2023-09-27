using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class PeerAssessmentItemRepository : IPeerAssessmentItemRepository
    {
        private readonly DapperContext _context;

        public PeerAssessmentItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePeerAssessmentItem(PeerAssessmentItem item)
        {
            var sql = "INSERT INTO [dbo].[PeerAssessmentItem] ([Question], [QuestionType], [Weight], [Target], [Required], [PeerAssessmentId]) " +
                      "VALUES (@Question, @QuestionType, @Weight, @Target, @Required, @PeerAssessmentId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, item);
            }
        }

        public async Task<IEnumerable<PeerAssessmentItemChoiceDto>> GetAllPeerAssessmentItems()
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Target,
                        i.Required,
                        i.PeerAssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.PeerAssessmentItemId AS PeerAssessmentItemId
                    FROM 
                        [dbo].[PeerAssessmentItem] AS i
                        LEFT JOIN [dbo].[PeerAssessmentChoice] AS c ON i.Id = c.PeerAssessmentItemId;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, PeerAssessmentItemChoiceDto>();

                var items = await connection.QueryAsync<PeerAssessmentItemChoiceDto, PeerAssessmentChoiceDto, PeerAssessmentItemChoiceDto>(
                            sql,
                            (item, choice) =>
                            {
                                if (itemDictionary.TryGetValue(item.Id, out var currentItem))
                                {
                                    if (choice != null)
                                    {
                                        currentItem.PeerAssessmentChoices.Add(choice);
                                    }
                                    return currentItem;
                                }
                                else
                                {
                                    item.PeerAssessmentChoices = new List<PeerAssessmentChoiceDto>();
                                    if (choice != null)
                                    {
                                        item.PeerAssessmentChoices.Add(choice);
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

        public async Task<PeerAssessmentItemChoiceDto> GetPeerAssessmentItemById(int id)
        {
            var sql = @"
                    SELECT 
                        i.Id, 
                        i.Question, 
                        i.QuestionType, 
                        i.Weight, 
                        i.Target,
                        i.Required,
                        i.PeerAssessmentId,
                        c.Id AS Id, 
                        c.ChoiceValue,
                        c.PeerAssessmentItemId AS PeerAssessmentItemId
                    FROM 
                        [dbo].[PeerAssessmentItem] AS i
                        LEFT JOIN [dbo].[PeerAssessmentChoice] AS c ON i.Id = c.PeerAssessmentItemId
                    WHERE 
                        i.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                var itemDictionary = new Dictionary<int, PeerAssessmentItemChoiceDto>();

                var item = await connection.QueryAsync<PeerAssessmentItemChoiceDto, PeerAssessmentChoiceDto, PeerAssessmentItemChoiceDto>(
                    sql,
                    (item, choice) =>
                    {
                        PeerAssessmentItemChoiceDto currentItem = null;

                        if (item != null && !itemDictionary.TryGetValue(item.Id, out currentItem))
                        {
                            currentItem = item;
                            currentItem.PeerAssessmentChoices = new List<PeerAssessmentChoiceDto>();
                            itemDictionary.Add(currentItem.Id, currentItem);
                        }

                        if (choice != null && currentItem != null)
                        {
                            currentItem.PeerAssessmentChoices.Add(choice);
                        }

                        return currentItem;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return item.FirstOrDefault();
            }
        }

        public async Task<int> UpdatePeerAssessmentItem(PeerAssessmentItem item)
        {
            var sql = "UPDATE [dbo].[PeerAssessmentItem] SET " +
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

        public async Task<int> DeletePeerAssessmentItem(int id)
        {
            var sql = "DELETE FROM [dbo].[PeerAssessmentItem] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}