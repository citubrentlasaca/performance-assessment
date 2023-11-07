using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;

namespace PerformanceAssessmentApi.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly DapperContext _context;

        public AnalyticsRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentId(int assessmentId)
        {
            var sql = @"SELECT u.FirstName, u.LastName, e.Id as EmployeeId, AVG(r.Score) AS AverageResult FROM [dbo].[Result] r " +
                        "INNER JOIN [dbo].[Employee] e ON r.EmployeeId = e.Id " +
                        "INNER JOIN [dbo].[User] u ON e.UserId = u.Id " +
                        "WHERE r.AssessmentId = @AssessmentId " +
                        "GROUP BY u.FirstName, u.LastName, e.Id " +
                        "ORDER BY AverageResult DESC;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<PerformanceDto>(sql, new { AssessmentId = assessmentId });
            }
        }

        public async Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndMonthNumber(int assessmentId, int monthNumber)
        {
            var sql = @"SELECT u.FirstName, u.LastName, e.Id as EmployeeId, AVG(r.Score) AS AverageResult " +
                        "FROM [dbo].[Result] r " +
                        "INNER JOIN [dbo].[Employee] e ON r.EmployeeId = e.Id " +
                        "INNER JOIN [dbo].[User] u ON e.UserId = u.Id " +
                        "WHERE r.AssessmentId = @AssessmentId " +
                        "AND DATEPART(MONTH, r.DateTimeDue) = @MonthNumber " +
                        "GROUP BY u.FirstName, u.LastName, e.Id " +
                        "ORDER BY AverageResult DESC;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<PerformanceDto>(sql, new { AssessmentId = assessmentId, MonthNumber = monthNumber });
            }
        }

        public async Task<IEnumerable<PerformanceDto>> GetEmployeesPerformanceByAssessmentIdAndYear(int assessmentId, int year)
        {
            var sql = @"SELECT u.FirstName, u.LastName, e.Id as EmployeeId, AVG(r.Score) AS AverageResult " +
                        "FROM [dbo].[Result] r " +
                        "INNER JOIN [dbo].[Employee] e ON r.EmployeeId = e.Id " +
                        "INNER JOIN [dbo].[User] u ON e.UserId = u.Id " +
                        "WHERE r.AssessmentId = @AssessmentId " +
                        "AND DATEPART(YEAR, r.DateTimeDue) = @Year " +
                        "GROUP BY u.FirstName, u.LastName, e.Id " +
                        "ORDER BY AverageResult DESC;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<PerformanceDto>(sql, new { AssessmentId = assessmentId, Year = year });
            }
        }
    }
}