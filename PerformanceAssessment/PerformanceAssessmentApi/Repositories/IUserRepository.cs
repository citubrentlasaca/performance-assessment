using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<User> GetUserByEmailAddressAndPassword(string email, string password);
        Task<int> UpdateUser(User user);
        Task<int> DeleteUser(int id);
    }
}