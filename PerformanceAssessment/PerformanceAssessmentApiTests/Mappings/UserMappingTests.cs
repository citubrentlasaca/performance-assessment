using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Mappings;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApiTests.Mappings
{
    public class UserMappingTests
    {
        private readonly IMapper _mapper;

        public UserMappingTests()
        {
            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new UserMappings()));
            mappingConfig.AssertConfigurationIsValid();
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Map_ValidUserCreationDto_ReturnsUser()
        {
            // Arrange
            var userCreationDto = new UserCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john@example.com",
                Password = "StrongPassword123"
            };

            // Act
            var user = _mapper.Map<UserDto>(userCreationDto);

            // Assert
            Assert.Equal(userCreationDto.FirstName, user.FirstName);
            Assert.Equal(userCreationDto.LastName, user.LastName);
            Assert.Equal(userCreationDto.EmailAddress, user.EmailAddress);
            Assert.Equal(userCreationDto.Password, user.Password);
            Assert.NotNull(user.DateTimeCreated);
            Assert.NotNull(user.DateTimeUpdated);
        }

        [Fact]
        public void Map_ValidUserUpdationDto_ReturnsUser()
        {
            // Arrange
            var userUpdationDto = new UserUpdationDto
            {
                FirstName = "David",
                LastName = "Lee",
                EmailAddress = "davidlee@example.com",
                Password = "WeakPassword123"
            };

            // Act
            var user = _mapper.Map<User>(userUpdationDto);

            // Assert
            Assert.Equal(userUpdationDto.FirstName, user.FirstName);
            Assert.Equal(userUpdationDto.LastName, user.LastName);
            Assert.Equal(userUpdationDto.EmailAddress, user.EmailAddress);
            Assert.Equal(userUpdationDto.Password, user.Password);
            Assert.NotNull(user.DateTimeUpdated);
        }
    }
}