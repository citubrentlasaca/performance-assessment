using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class AssignSchedulerRepository : IAssignSchedulerRepository
    {
        private readonly DapperContext _context;

        public AssignSchedulerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> CreateAssignSchedulers(IEnumerable<int> employeeIds, AssignScheduler assignScheduler)
        {
            var sql = "INSERT INTO [dbo].[AssignScheduler] ([AssessmentId], [EmployeeId], [IsAnswered], [DueDate], [Time], [DateTimeCreated], [DateTimeUpdated]) " +
                      "VALUES (@AssessmentId, @EmployeeId, @IsAnswered, @DueDate, @Time, @DateTimeCreated, @DateTimeUpdated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                var insertedIds = new List<int>();

                foreach (var employeeId in employeeIds)
                {
                    assignScheduler.EmployeeId = employeeId;

                    var insertedId = await con.ExecuteScalarAsync<int>(sql, assignScheduler);
                    insertedIds.Add(insertedId);
                }

                return insertedIds;
            }
        }

        public async Task<IEnumerable<AssignSchedulerDto>> GetAllAssignSchedulers()
        {
            var sql = "SELECT * FROM [dbo].[AssignScheduler];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AssignSchedulerDto>(sql);
            }
        }

        public async Task<AssignSchedulerDto> GetAssignSchedulerById(int id)
        {
            var sql = "SELECT * FROM [dbo].[AssignScheduler] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<AssignSchedulerDto>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByAssessmentId(int assessmentId)
        {
            var sql = "SELECT * FROM [dbo].[AssignScheduler] WHERE [AssessmentId] = @AssessmentId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AssignSchedulerDto>(sql, new { AssessmentId = assessmentId });
            }
        }

        public async Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByEmployeeId(int employeeId)
        {
            var sql = "SELECT * FROM [dbo].[AssignScheduler] WHERE [EmployeeId] = @EmployeeId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<AssignSchedulerDto>(sql, new { EmployeeId = employeeId });
            }
        }

        public async Task<int> UpdateAssignScheduler(AssignScheduler assignScheduler)
        {
            var sql = "UPDATE [dbo].[AssignScheduler] SET " +
                      "[AssessmentId] = @AssessmentId, " +
                      "[EmployeeId] = @EmployeeId, " +
                      "[IsAnswered] = @IsAnswered, " +
                      "[DueDate] = @DueDate, " +
                      "[Time] = @Time, " +
                      "[DateTimeUpdated] = @DateTimeUpdated " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = assignScheduler.Id,
                        AssessmentId = assignScheduler.AssessmentId,
                        EmployeeId = assignScheduler.EmployeeId,
                        IsAnswered = assignScheduler.IsAnswered,
                        DueDate = assignScheduler.DueDate,
                        Time = assignScheduler.Time,
                        DateTimeUpdated = assignScheduler.DateTimeUpdated
                    }
                );
            }
        }

        public async Task<int> DeleteAssignScheduler(int id)
        {
            var sql = "DELETE FROM [dbo].[AssignScheduler] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }

        public async Task<AssignSchedulerDto> GetAssignSchedulerByEmployeeIdAndAssessmentId(int employeeId, int assessmentId)
        {
            var sql = "SELECT * FROM [dbo].[AssignScheduler] WHERE [EmployeeId] = @EmployeeId AND [AssessmentId] = @AssessmentId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<AssignSchedulerDto>(sql, new { EmployeeId = employeeId, AssessmentId = assessmentId });
            }
        }
    }
}