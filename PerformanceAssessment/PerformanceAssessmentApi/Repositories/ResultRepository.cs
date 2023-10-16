using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly DapperContext _context;

        public ResultRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateResult(Result result)
        {
            var sql = "INSERT INTO [dbo].[Result] ([AssessmentId], [EmployeeId], [Score], [DateTimeDue], [DateTimeCreated], [DateTimeUpdated]) " +
                      "VALUES (@AssessmentId, @EmployeeId, @Score, @DateTimeDue, @DateTimeCreated, @DateTimeUpdated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, result);
            }
        }

        public async Task<IEnumerable<ResultDto>> GetAllResults()
        {
            var sql = "SELECT * FROM [dbo].[Result];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<ResultDto>(sql);
            }
        }

        public async Task<ResultDto> GetResultById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Result] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<ResultDto>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<ResultDto>> GetResultByAssessmentId(int assessmentId)
        {
            var sql = "SELECT * FROM [dbo].[Result] WHERE [AssessmentId] = @AssessmentId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<ResultDto>(sql, new { AssessmentId = assessmentId });
            }
        }

        public async Task<IEnumerable<ResultDto>> GetResultByEmployeeId(int employeeId)
        {
            var sql = "SELECT * FROM [dbo].[Result] WHERE [EmployeeId] = @EmployeeId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<ResultDto>(sql, new { EmployeeId = employeeId });
            }
        }

        public async Task<int> UpdateResult(Result result)
        {
            var sql = "UPDATE [dbo].[Result] SET " +
                      "[AssessmentId] = @AssessmentId, " +
                      "[EmployeeId] = @EmployeeId, " +
                      "[Score] = @Score, " +
                      "[DateTimeDue] = @DateTimeDue, " +
                      "[DateTimeUpdated] = @DateTimeUpdated " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = result.Id,
                        AssessmentId = result.AssessmentId,
                        EmployeeId = result.EmployeeId,
                        Score = result.Score,
                        DateTimeDue = result.DateTimeDue,
                        DateTimeUpdated = result.DateTimeUpdated
                    }
                );
            }
        }

        public async Task<int> DeleteResult(int id)
        {
            var sql = "DELETE FROM [dbo].[Result] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}