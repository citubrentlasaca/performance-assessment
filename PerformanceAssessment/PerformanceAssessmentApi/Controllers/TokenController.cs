using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/tokens")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private static User _user = new User();
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public TokenController(ILogger<UserController> logger, ITokenService tokenService, IUserService userService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userService = userService;
        }

        /// <summary>
        /// Creates a new token for a user based on their credentials
        /// </summary>
        /// <param name="userLogin">User credentials (email address and password)</param>
        /// <returns>Returns a token pair (access token and refresh token)</returns>
        [HttpPost("create")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateToken([FromBody] UserLoginDto userLogin)
        {
            try
            {
                // Authenticate the user and get their information
                _user = await _userService.GetUserByEmailAddressAndPassword(userLogin.EmailAddress, userLogin.Password);

                // Create a new access token and a refresh token
                string accessToken = _tokenService.CreateToken(_user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                // Set the new refresh token and return the token pair
                SetRefreshToken(refreshToken);

                var tokenDto = new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                };

                return Ok(tokenDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Refreshes an existing access token using a valid refresh token
        /// </summary>
        /// <param name="request">Request with a valid refresh token</param>
        /// <returns>Returns a new token pair (access token and refresh token)</returns>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RefreshToken([FromBody] RenewTokenDto request)
        {
            string tokenStatus = _tokenService.VerifyToken(request.RefreshToken, _user);

            if (tokenStatus == "Invalid Token")
            {
                return Unauthorized("Invalid refresh token");
            }
            else if (tokenStatus == "Expired Token")
            {
                return Unauthorized("Refresh token expired");
            }

            // Create a new access token and a refresh token
            string accessToken = _tokenService.CreateToken(_user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Set the new refresh token and return the token pair
            SetRefreshToken(newRefreshToken);

            var tokenDto = new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token
            };

            return Ok(tokenDto);
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            _user.RefreshToken = newRefreshToken.Token;
            _user.TokenCreated = newRefreshToken.Created;
            _user.TokenExpires = newRefreshToken.Expires;
        }
    }
}