﻿using Dapper;
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

        public async Task<int> CreateEmployeeNotification(EmployeeAssignSchedulerNotification employeeNotification)
        {
            var sql = "INSERT INTO [dbo].[EmployeeAssignSchedulerNotification] ([EmployeeId], [AssessmentId], [DateTimeCreated]) " +
                      "VALUES (@EmployeeId, @AssessmentId, @DateTimeCreated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, employeeNotification);
            }
        }

        public async Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotifications()
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAssignSchedulerNotification];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeAssignSchedulerNotificationDto>(sql);
            }
        }

        public async Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotificationsByEmployeeId(int employeeId)
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAssignSchedulerNotification] WHERE [EmployeeId] = @EmployeeId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeAssignSchedulerNotificationDto>(sql, new { EmployeeId = employeeId });
            }
        }

        public async Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeNotificationById(int id)
        {
            var sql = "SELECT * FROM [dbo].[EmployeeAssignSchedulerNotification] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeAssignSchedulerNotificationDto>(sql, new { Id = id });
            }
        }
    }
}