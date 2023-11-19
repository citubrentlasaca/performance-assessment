using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class EmployeeAnnouncementNotificationRepository : IEmployeeAnnouncementNotificationRepository
    {
        private readonly DapperContext _context;

        public EmployeeAnnouncementNotificationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployeeAnnouncementNotification(EmployeeAnnouncementNotification employeeNotification)
        {
            var sql = "INSERT INTO [dbo].[EmployeeAnnouncementNotification] ([EmployeeId], [AnnouncementId], [DateTimeCreated]) " +
          "VALUES (@EmployeeId, @AnnouncementId, @DateTimeCreated); " +
          "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, employeeNotification);
            }
        }

        public async Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetAllEmployeeAnnouncementNotifications()
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAnnouncementNotification];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeAnnouncementNotificationDto>(sql);
            }
        }

        public async Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetEmployeeAnnouncementNotificationByEmployeeId(int id)
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAnnouncementNotification] WHERE [EmployeeId] = @Id;";

            using (var con = _context.CreateConnection())
            { 
                return await con.QueryAsync<EmployeeAnnouncementNotificationDto>(sql, new { Id = id });
            }
        }

        public async Task<EmployeeAnnouncementNotificationDto> GetEmployeeAnnouncementNotificationById(int id)
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAnnouncementNotification] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeAnnouncementNotificationDto>(sql, new { Id = id });
            }
        }
    }
}
