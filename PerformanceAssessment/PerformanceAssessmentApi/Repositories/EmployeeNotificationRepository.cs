using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class EmployeeNotificationRepository : IEmployeeNotificationRepository
    {
        private readonly DapperContext _context;

        public EmployeeNotificationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployeeNotification(EmployeeNotification employeeNotification)
        {
            var sql = "INSERT INTO [dbo].[EmployeeNotification] ([EmployeeId], [AssessmentId], [AnnouncementId], [DateTimeCreated]) " +
                      "VALUES (@EmployeeId, @AssessmentId, @AnnouncementId, @DateTimeCreated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, employeeNotification);
            }
        }

        public async Task<IEnumerable<EmployeeNotificationDto>> GetAllEmployeeNotifications()
        {
            var sql = "SELECT * FROM [dbo].[EmployeeNotification];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeNotificationDto>(sql);
            }
        }

        public async Task<EmployeeNotificationDto> GetEmployeeNotificationById(int id)
        {
            var sql = "SELECT * FROM [dbo].[EmployeeNotification] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeNotificationDto>(sql, new { Id = id });
            }
        }
    }
}