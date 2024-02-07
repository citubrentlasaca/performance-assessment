using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class EmployeeAssignSchedulerNotificationRepository : IEmployeeAssignSchedulerNotificationRepository
    {
        private readonly DapperContext _context;

        public EmployeeAssignSchedulerNotificationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployeeAssignSchedulerNotification(EmployeeAssignSchedulerNotification employeeNotification)
        {
            var sql = "INSERT INTO [dbo].[EmployeeAssignSchedulerNotification] ([EmployeeId], [AssessmentId], [IsRead], [DateTimeCreated]) " +
                      "VALUES (@EmployeeId, @AssessmentId, 0, @DateTimeCreated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, employeeNotification);
            }
        }

        public async Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotifications()
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAssignSchedulerNotification];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeAssignSchedulerNotificationDto>(sql);
            }
        }

        public async Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(int employeeId)
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAssignSchedulerNotification] WHERE [EmployeeId] = @EmployeeId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeAssignSchedulerNotificationDto>(sql, new { EmployeeId = employeeId });
            }
        }

        public async Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeAssignSchedulerNotificationById(int id)
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAssignSchedulerNotification] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeAssignSchedulerNotificationDto>(sql, new { Id = id });
            }
        }

        public async Task<int> MarkEmployeeAssignSchedulerNotificationAsRead(int id)
        {
            var sql = "UPDATE [dbo].[EmployeeAssignSchedulerNotification] SET [IsRead] = 1 WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}