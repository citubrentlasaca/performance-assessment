using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class PeerAssessmentChoiceRepository : IPeerAssessmentChoiceRepository
    {
        private readonly DapperContext _context;

        public PeerAssessmentChoiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePeerAssessmentChoice(PeerAssessmentChoice choice)
        {
            var sql = "INSERT INTO [dbo].[PeerAssessmentChoice] ([ChoiceValue], [PeerAssessmentItemId]) " +
                      "VALUES (@ChoiceValue, @PeerAssessmentItemId); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, choice);
            }
        }

        public async Task<IEnumerable<PeerAssessmentChoiceDto>> GetAllPeerAssessmentChoices()
        {
            var sql = "SELECT * FROM [dbo].[PeerAssessmentChoice];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<PeerAssessmentChoiceDto>(sql);
            }
        }

        public async Task<PeerAssessmentChoiceDto> GetPeerAssessmentChoiceById(int id)
        {
            var sql = "SELECT * FROM [dbo].[PeerAssessmentChoice] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<PeerAssessmentChoiceDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdatePeerAssessmentChoice(PeerAssessmentChoice choice)
        {
            var sql = "UPDATE [dbo].[PeerAssessmentChoice] SET " +
                      "[ChoiceValue] = @ChoiceValue " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = choice.Id,
                        ChoiceValue = choice.ChoiceValue
                    }
                );
            }
        }

        public async Task<int> DeletePeerAssessmentChoice(int id)
        {
            var sql = "DELETE FROM [dbo].[PeerAssessmentChoice] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}