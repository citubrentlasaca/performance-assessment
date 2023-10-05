using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUser(User user)
        {
            var sql = "INSERT INTO [dbo].[User] ([FirstName], [LastName], [EmailAddress], [Password], [DateTimeCreated], [DateTimeUpdated]) " +
                      "VALUES (@FirstName, @LastName, @EmailAddress, @Password, @DateTimeCreated, @DateTimeUpdated); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, user);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var sql = "SELECT * FROM [dbo].[User];";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<UserDto>(sql);
            }
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var sql = "SELECT * FROM [dbo].[User] WHERE [Id] = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<UserDto>(sql, new { Id = id });
            }
        }

        public async Task<User> GetUserByEmailAddressAndPassword(string email, string password)
        {
            var sql = "SELECT * FROM [dbo].[User] WHERE [EmailAddress] = @EmailAddress AND [Password] = @Password;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryFirstOrDefaultAsync<User>(sql, new { EmailAddress = email, Password = password });
            }
        }

        public async Task<int> UpdateUser(User user)
        {
            var sql = "UPDATE [dbo].[User] SET " +
                      "[FirstName] = @FirstName, " +
                      "[LastName] = @LastName, " +
                      "[EmailAddress] = @EmailAddress, " +
                      "[Password] = @Password, " +
                      "[DateTimeUpdated] = @DateTimeUpdated " +
                      "WHERE Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        DateTimeUpdated = user.DateTimeUpdated
                    }
                );
            }
        }

        public async Task<int> DeleteUser(int id)
        {
            var sql = "DELETE FROM [dbo].[User] WHERE [Id] = @Id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = id });
            }
        }
    }
}