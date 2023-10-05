using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(UserCreationDto user)
        {
            var model = _mapper.Map<User>(user);
            model.Id = await _repository.CreateUser(model);

            return model;
        }

        public Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public Task<UserDto> GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        public Task<User> GetUserByEmailAddressAndPassword(string email, string password)
        {
            return _repository.GetUserByEmailAddressAndPassword(email, password);
        }

        public async Task<int> UpdateUser(int id, UserUpdationDto user)
        {
            var model = _mapper.Map<User>(user);
            model.Id = id;

            return await _repository.UpdateUser(model);
        }

        public async Task<int> DeleteUser(int id)
        {
            return await _repository.DeleteUser(id);
        }
    }
}