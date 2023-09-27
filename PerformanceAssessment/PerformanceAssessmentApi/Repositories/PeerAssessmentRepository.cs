using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using System.Data;

namespace PerformanceAssessmentApi.Repositories
{
    public class PeerAssessmentRepository : IPeerAssessmentRepository
    {
        private readonly DapperContext _context;

        public PeerAssessmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePeerAssessment(PeerAssessment assessment)
        {
            var sql = "INSERT INTO [dbo].[PeerAssessment] ([Title], [Description]) " +
                      "VALUES (@Title, @Description); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, assessment);
            }
        }

        public async Task<IEnumerable<PeerAssessmentDto>> GetAllPeerAssessments()
        {
            var sql = "SELECT * FROM [dbo].[PeerAssessment];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<PeerAssessmentDto>(sql);
            }
        }

        public async Task<PeerAssessmentDto> GetPeerAssessmentById(int id)
        {
            var sql = "SELECT * FROM [dbo].[PeerAssessment] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<PeerAssessmentDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdatePeerAssessment(PeerAssessment assessment)
        {
            var sql = "UPDATE [dbo].[PeerAssessment] SET " +
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

        public async Task<int> DeletePeerAssessment(int id)
        {
            var sql = "DELETE FROM [dbo].[PeerAssessment] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }

        public async Task<PeerAssessmentItemsDto?> GetPeerAssessmentItemsById(int id)
        {
            var assessmentSql = "SELECT * FROM [dbo].[PeerAssessment] WHERE [Id] = @Id;";
            var itemsSql = "SELECT * FROM [dbo].[PeerAssessmentItem] WHERE [PeerAssessmentId] = @Id;";
            var choicesSql = "SELECT * FROM [dbo].[PeerAssessmentChoice] WHERE [PeerAssessmentItemId] IN (SELECT [Id] FROM [dbo].[PeerAssessmentItem] WHERE [PeerAssessmentId] = @Id);";

            using (var con = _context.CreateConnection())
            {
                using (var multi = await con.QueryMultipleAsync($"{assessmentSql}{itemsSql}{choicesSql}", new { Id = id }))
                {
                    var assessment = await multi.ReadSingleOrDefaultAsync<PeerAssessmentItemsDto>();

                    if (assessment != null)
                    {
                        assessment.PeerAssessmentItems = (await multi.ReadAsync<PeerAssessmentItemDto>()).ToList();
                        var choices = (await multi.ReadAsync<PeerAssessmentChoiceDto>()).ToList();

                        foreach (var item in assessment.PeerAssessmentItems)
                        {
                            item.PeerAssessmentChoices = choices.Where(choice => choice.PeerAssessmentItemId == item.Id).ToList();
                        }
                    }
                    return assessment;
                }
            }
        }
    }
}