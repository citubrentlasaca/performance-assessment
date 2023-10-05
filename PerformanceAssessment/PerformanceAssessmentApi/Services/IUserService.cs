using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(UserCreationDto user);

        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UserDto> GetUserById(int id);

        Task<int> UpdateUser(int id, UserUpdationDto user);

        Task<int> DeleteUser(int id);
    }
}