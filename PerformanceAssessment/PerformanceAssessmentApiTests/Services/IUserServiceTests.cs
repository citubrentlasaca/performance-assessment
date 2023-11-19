using AutoMapper;
using Moq;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApiTests.Services
{
    public class IUserServiceTests
    {
        private readonly Mock<IUserRepository> _fakeRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly IUserService _userService;

        public IUserServiceTests()
        {
            _fakeRepository = new Mock<IUserRepository>();
            _fakeMapper = new Mock<IMapper>();
            _userService = new UserService(_fakeRepository.Object, _fakeMapper.Object);
        }

        [Fact]
        public async Task CreateUser_ValidObjectPassed_ReturnsCreatedUser()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var userDto = new UserCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@example.com",
                Password = "abc123"
            };

            var user = new UserDto
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@example.com",
                Password = "abc123"
            };

            _fakeMapper.Setup(x => x.Map<UserDto>(userDto)).Returns(user);
            _fakeRepository.Setup(x => x.CreateUser(user)).ReturnsAsync(userId);

            // Act
            var result = await _userService.CreateUser(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(userDto.FirstName, result.FirstName);
            Assert.Equal(userDto.LastName, result.LastName);
            Assert.Equal(userDto.EmailAddress, result.EmailAddress);
            Assert.Equal(userDto.Password, result.Password);
        }

        [Fact]
        public async Task CreateUser_DuplicateEmailAddress_ThrowsException()
        {
            // Arrange
            var userDto = new UserCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@example.com",
                Password = "abc123"
            };

            _fakeRepository.Setup(x => x.CheckIfUserExists(userDto.EmailAddress)).ReturnsAsync(true);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.CreateUser(userDto));
        }

        [Fact]
        public async Task GetAllUsers_ExistingUsers_ReturnsAllUsers()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var users = new List<UserDto>
            {
                new UserDto { Id = userId, FirstName = "John", LastName = "Doe", EmailAddress = "johndoe@example.com", Password = "password123" },
                new UserDto { Id = userId, FirstName = "Jane", LastName = "Doe", EmailAddress = "janedoe@example.com", Password = "password456" },
            };

            _fakeRepository.Setup(x => x.GetAllUsers()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count(), result.Count());
        }

        [Fact]
        public async Task GetUserById_ExistingUser_ReturnsUser()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var userDto = new UserDto
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@example.com",
                Password = "password123"
            };

            _fakeRepository.Setup(x => x.GetUserById(userId)).ReturnsAsync(userDto);

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(userDto.FirstName, result.FirstName);
            Assert.Equal(userDto.LastName, result.LastName);
            Assert.Equal(userDto.EmailAddress, result.EmailAddress);
            Assert.Equal(userDto.Password, result.Password);
        }

        [Fact]
        public async Task GetUserByEmailAddressAndPassword_ExistingUser_ReturnsUser()
        {
            // Arrange
            var userId = It.IsAny<int>();
            var email = "johndoe@example.com";
            var password = "password123";

            var user = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = email,
                Password = password
            };

            _fakeRepository.Setup(x => x.GetUserByEmailAddressAndPassword(email, password)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByEmailAddressAndPassword(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.EmailAddress, result.EmailAddress);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task UpdateUser_ExistingUser_ReturnsNumberOfUpdatedUsers()
        {
            // Arrange
            var userId = It.IsAny<int>();

            var userDto = new UserUpdationDto
            {
                FirstName = "John",
                LastName = "Smith",
                EmailAddress = "john.smith@example.com",
                Password = "newPassword"
            };

            var user = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Smith",
                EmailAddress = "john.smith@example.com",
                Password = "newPassword"
            };

            _fakeMapper.Setup(x => x.Map<User>(userDto)).Returns(user);
            _fakeRepository.Setup(x => x.UpdateUser(user)).ReturnsAsync(1);

            // Act
            var result = await _userService.UpdateUser(userId, userDto);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteUser_ExistingUser_ReturnsNumberOfDeletedUsers()
        {
            // Arrange
            var userId = It.IsAny<int>();

            _fakeRepository.Setup(x => x.DeleteUser(userId)).ReturnsAsync(1);

            // Act
            var result = await _userService.DeleteUser(userId);

            // Assert
            Assert.Equal(1, result);
        }
    }
}