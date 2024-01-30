using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class AdminNotificationRepository : IAdminNotificationRepository
    {
        private readonly DapperContext _context;

        public AdminNotificationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAdminNotification(AdminNotification adminNotification)
        {
            var sql = "INSERT INTO [dbo].[AdminNotification] ([EmployeeId], [EmployeeName], [AssessmentTitle], [TeamName], [IsRead], [DateTimeCreated]) " +
                      "VALUES (@EmployeeId, @EmployeeName, @AssessmentTitle, @TeamName, 0, @DateTimeCreated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, adminNotification);
            }
        }

        public async Task<IEnumerable<AdminNotificationDto>> GetAllAdminNotifications()
        {
            var sql = "SELECT * FROM [dbo].[AdminNotification];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AdminNotificationDto>(sql);
            }
        }

        public async Task<AdminNotificationDto> GetAdminNotificationById(int id)
        {
            var sql = "SELECT * FROM [dbo].[AdminNotification] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<AdminNotificationDto>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<AdminNotificationDto>> GetAdminNotificationByEmployeeId(int employeeId)
        {
            var sql = "SELECT * FROM [dbo].[AdminNotification] WHERE [EmployeeId] = @EmployeeId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AdminNotificationDto>(sql, new { EmployeeId = employeeId });
            }
        }

        public async Task<int> MarkAdminNotificationAsRead(int id)
        {
            var sql = "UPDATE [dbo].[AdminNotification] SET [IsRead] = 1 WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}