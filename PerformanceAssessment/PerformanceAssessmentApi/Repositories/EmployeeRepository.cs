using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeByUserIdAndTeamId(int userId, int teamId)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [UserId] = @UserId AND [TeamId] = @TeamId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<Employee>(sql, new { UserId = userId, TeamId = teamId });
            }
        }

        public async Task<int> CreateEmployee(Employee employee)
        {
            var sql = "INSERT INTO [dbo].[Employee] ([UserId], [TeamId], [Role], [Status], [DateTimeJoined]) " +
                      "VALUES (@UserId, @TeamId, @Role, @Status, @DateTimeJoined); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                employee.Role = "Admin";
                employee.Status = "Active";

                var existingEmployee = await GetEmployeeByUserIdAndTeamId(employee.UserId, employee.TeamId);

                if (existingEmployee != null)
                {
                    throw new Exception("User is already in the specified team");
                }

                return await con.ExecuteScalarAsync<int>(sql, employee);
            }
        }

        public async Task<EmployeeDto> GetEmployeeByUserIdAndTeamCode(int userId, Guid teamCode)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [UserId] = @UserId AND [TeamId] = (SELECT [Id] FROM [dbo].[Team] WHERE [TeamCode] = @TeamCode);";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeDto>(sql, new { UserId = userId, TeamCode = teamCode });
            }
        }

        public async Task<int> CreateEmployeeWithTeamCode(EmployeeTeamInfoDto employee)
        {
            try
            {
                var teamCode = employee.TeamCode;
                var teamIdSql = "SELECT TOP 1 [Id] FROM [dbo].[Team] WHERE [TeamCode] = @TeamCode;";

                using (var con = _context.CreateConnection())
                {
                    var teamId = await con.ExecuteScalarAsync<int>(teamIdSql, new { TeamCode = teamCode });

                    if (teamId == 0)
                    {
                        throw new Exception("Team not found");
                    }

                    // Check if the user is already in the specified team
                    var existingEmployee = await GetEmployeeByUserIdAndTeamCode(employee.UserId, teamCode);

                    if (existingEmployee != null)
                    {
                        throw new Exception("User is already in the specified team");
                    }

                    var sql = "INSERT INTO [dbo].[Employee] ([UserId], [TeamId], [Role], [Status], [DateTimeJoined]) " +
                              "VALUES (@UserId, @TeamId, @Role, @Status, @DateTimeJoined); " +
                              "SELECT SCOPE_IDENTITY();";

                    return await con.ExecuteScalarAsync<int>(sql, new
                    {
                        UserId = employee.UserId,
                        TeamId = teamId,
                        Role = employee.Role,
                        Status = employee.Status,
                        DateTimeJoined = StringUtil.GetCurrentDateTime()
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors as needed
                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var sql = "SELECT * FROM [dbo].[Employee];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeDto>(sql);
            }
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeDto>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByUserId(int userId)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [UserId] = @UserId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeDto>(sql, new { UserId = userId });
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByTeamId(int teamId)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [TeamId] = @TeamId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<EmployeeDto>(sql, new { TeamId = teamId });
            }
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            var sql = "UPDATE [dbo].[Employee] SET " +
                      "[UserId] = @UserId, " +
                      "[TeamId] = @TeamId, " +
                      "[Role] = @Role, " +
                      "[Status] = @Status " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = employee.Id,
                        UserId = employee.UserId,
                        TeamId = employee.TeamId,
                        Role = employee.Role,
                        Status = employee.Status
                    }
                );
            }
        }

        public async Task<int> DeleteEmployee(int id)
        {
            var sql = "DELETE FROM [dbo].[Employee] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }

        public async Task<EmployeeDetailsDto> GetEmployeeDetailsById(int id)
        {
            var sql = @"
                      SELECT e.*, u.*, t.*
                      FROM [dbo].[Employee] AS e 
                      LEFT JOIN [dbo].[User] AS u ON e.UserId = u.Id
                      LEFT JOIN [dbo].[Team] AS t ON e.TeamId = t.Id
                      WHERE e.Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryAsync<EmployeeDetailsDto, UserDto, TeamDto, EmployeeDetailsDto>(
                    sql,
                    (employeeDetails, user, team) =>
                    {
                        employeeDetails.Users = new List<UserDto> { user };
                        employeeDetails.Teams = new List<TeamDto> { team };
                        return employeeDetails;
                    },
                    new { Id = id },
                    splitOn: "Id, Id"
                );
                return result.FirstOrDefault();
            }
        }

        public async Task<EmployeeDto> GetEmployeeByTeamIdAndUserId(int teamId, int userId)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [TeamId] = @TeamId AND [UserId] = @UserId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeDto>(sql, new { TeamId = teamId, UserId = userId });
            }
        }
    }
}