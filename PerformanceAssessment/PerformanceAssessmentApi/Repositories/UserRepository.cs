using Dapper;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUser(UserDto user)
        {
            // Generate a random salt
            (user.Password, user.Salt) = PasswordHasher.HashPassword(user.Password);

            var sql = "INSERT INTO [dbo].[User] ([FirstName], [LastName], [EmailAddress], [Password], [Salt], [DateTimeCreated], [DateTimeUpdated]) " +
                      "VALUES (@FirstName, @LastName, @EmailAddress, @Password, @Salt, @DateTimeCreated, @DateTimeUpdated); " +
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
            var sql = "SELECT * FROM [dbo].[User] WHERE [EmailAddress] = @EmailAddress;";

            using (var con = _context.CreateConnection())
            {
                var user = await con.QuerySingleOrDefaultAsync<User>(sql, new { EmailAddress = email });

                if (user != null && PasswordHasher.VerifyPassword(password, user.Password, user.Salt))
                {
                    return user;
                }
                return null; // Password doesn't match or user not found
            }
        }

        public async Task<int> UpdateUser(User user)
        {
            var sql = "UPDATE [dbo].[User] SET " +
                      "[FirstName] = @FirstName, " +
                      "[LastName] = @LastName, " +
                      "[EmailAddress] = @EmailAddress, " +
                      "[ProfilePicture] = @ProfilePicture, " +
                      "[DateTimeUpdated] = @DateTimeUpdated ";

            // Check if the password is being updated
            if (!string.IsNullOrEmpty(user.Password))
            {
                // Generate a new salt and hash for the new password
                (user.Password, user.Salt) = PasswordHasher.HashPassword(user.Password);
                sql += ", [Password] = @Password, [Salt] = @Salt ";
            }

            sql += "WHERE Id = @Id;";

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
                        Salt = user.Salt,
                        ProfilePicture = user.ProfilePicture,
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

        public async Task<bool> CheckIfUserExists(string emailAddress)
        {
            var sql = "SELECT 1 FROM [dbo].[User] WHERE [EmailAddress] = @EmailAddress;";

            using (var con = _context.CreateConnection())
            {
                var result = await con.ExecuteScalarAsync<int>(sql, new { EmailAddress = emailAddress });
                return result == 1;
            }
        }
    }
}