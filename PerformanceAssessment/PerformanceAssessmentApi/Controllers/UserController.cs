using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">User details</param>
        /// <returns>Returns the newly created user</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users
        ///     {
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "emailAddress": "johndoe@email.com",
        ///         "password": "StrongPassword123#%!"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new user</response>
        /// <response code="400">User details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateUser")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto user)
        {
            try
            {
                // Create a new user
                var newUser = await _userService.CreateUser(user);

                return CreatedAtRoute("GetUserById", new { id = newUser.Id }, newUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Returns all users</returns>
        /// <response code="200">Users found</response>
        /// <response code="204">No users found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllUsers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                if (users.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns the details of the user with id <paramref name="id"/></returns>
        /// <response code="200">User found</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                // Check if user exists
                var foundUser = await _userService.GetUserById(id);

                if (foundUser == null)
                {
                    return StatusCode(404, "User not found");
                }

                return Ok(foundUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the user by email address and password
        /// </summary>
        /// <param name="email">Email Address of the User</param>
        /// <param name="password">Password of the User</param>
        /// <returns>Returns the details of the user</returns>
        /// <response code="200">User found</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("authenticate", Name = "GetUserByEmailAddressAndPassword")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByEmailAddressAndPassword(string email, string password)
        {
            try
            {
                // Retrieve the user by username and password
                var foundUser = await _userService.GetUserByEmailAddressAndPassword(email, password);

                if (foundUser == null)
                {
                    return StatusCode(404, "User not found");
                }

                return Ok(foundUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="id">The id of the user that will be updated</param>
        /// <param name="user">New user details</param>
        /// <returns>Returns the details of the user with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users
        ///     {
        ///         "firstName": "Jane",
        ///         "lastName": "Dawn",
        ///         "emailAddress": "janedawn@email.com",
        ///         "password": "WeakPassword123456"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the user</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateUser")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdationDto user)
        {
            try
            {
                // Check if user exists
                var foundUser = await _userService.GetUserById(id);

                if (foundUser == null)
                {
                    return StatusCode(404, "User not found");
                }

                // Update the user
                await _userService.UpdateUser(id, user);
                return Ok("User updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing user
        /// </summary>
        /// <param name="id">The id of the user that will be deleted</param>
        /// <response code="200">Successfully deleted the user</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteUser")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                // Check if user exists
                var foundUser = await _userService.GetUserById(id);

                if (foundUser == null)
                {
                    return StatusCode(404, "User not found");
                }

                await _userService.DeleteUser(id);
                return Ok("User deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}