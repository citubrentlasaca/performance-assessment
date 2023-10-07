﻿using Dapper;
using Microsoft.Data.SqlClient;
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

        public async Task<int> CreateEmployee(Employee employee)
        {
            var sql = "INSERT INTO [dbo].[Employee] ([UserId], [TeamId], [Status], [DateTimeJoined]) " +
                      "VALUES (@UserId, @TeamId, @Status, @DateTimeJoined); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                employee.Status = "Active";
                return await con.ExecuteScalarAsync<int>(sql, employee);
            }
        }

        public async Task<int> CreateEmployeeWithTeamCode(EmployeeTeamInfoDto employee)
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

                var sql = "INSERT INTO [dbo].[Employee] ([UserId], [TeamId], [Status], [DateTimeJoined]) " +
                          "VALUES (@UserId, (SELECT TOP 1 [Id] FROM [dbo].[Team] WHERE [TeamCode] = @TeamCode), @Status, @DateTimeJoined); " +
                          "SELECT SCOPE_IDENTITY();";

                return await con.ExecuteScalarAsync<int>(sql, new
                {
                    UserId = employee.UserId,
                    TeamCode = teamCode,
                    Status = employee.Status,
                    DateTimeJoined = StringUtil.GetCurrentDateTime()
                });
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

        public async Task<EmployeeDto> GetEmployeeByUserId(int userId)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [UserId] = @UserId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeDto>(sql, new { UserId = userId });
            }
        }

        public async Task<EmployeeDto> GetEmployeeByTeamId(int teamId)
        {
            var sql = "SELECT * FROM [dbo].[Employee] WHERE [TeamId] = @TeamId;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<EmployeeDto>(sql, new { TeamId = teamId });
            }
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            var sql = "UPDATE [dbo].[Employee] SET " +
                      "[UserId] = @UserId, " +
                      "[TeamId] = @TeamId, " +
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
    }
}