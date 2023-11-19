using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly DapperContext _context;

        public AnnouncementRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAnnouncement(Announcement announcement)
        {
            var sql = "INSERT INTO [dbo].[Announcement] ([TeamId], [Content], [DateTimeCreated], [DateTimeUpdated]) " +
                      "VALUES (@TeamId, @Content, @DateTimeCreated, @DateTimeUpdated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, announcement);
            }
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAnnouncements()
        {
            var sql = "SELECT * FROM [dbo].[Announcement];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AnnouncementDto>(sql);
            }
        }

        public async Task<AnnouncementDto> GetAnnouncementById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Announcement] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<AnnouncementDto>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateAnnouncement(Announcement announcement)
        {
            var sql = "UPDATE [dbo].[Announcement] SET " +
                      "[Content] = @Content, " +
                      "[DateTimeUpdated] = @DateTimeUpdated " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = announcement.Id,
                        Content = announcement.Content,
                        DateTimeUpdated = announcement.DateTimeUpdated
                    }
                );
            }
        }

        public async Task<int> DeleteAnnouncement(int id)
        {
            var sql = "DELETE FROM [dbo].[Announcement] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}