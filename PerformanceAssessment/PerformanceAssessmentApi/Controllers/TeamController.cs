using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogger<TeamController> _logger;

        public TeamController(ITeamService teamService, ILogger<TeamController> logger)
        {
            _teamService = teamService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="team">Team details</param>
        /// <returns>Returns the newly created team</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/teams
        ///     {
        ///         "organization": "Acme Inc.",
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "businessType": "Technology",
        ///         "businessAddress": "123 Main Street"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new team</response>
        /// <response code="400">Team details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateTeam")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTeam([FromBody] TeamCreationDto team)
        {
            try
            {
                // Create a new team
                var newTeam = await _teamService.CreateTeam(team);

                return CreatedAtRoute("GetTeamById", new { id = newTeam }, newTeam);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all teams
        /// </summary>
        /// <returns>Returns all teams</returns>
        /// <response code="200">Teams found</response>
        /// <response code="204">No teams found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllTeams")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTeams()
        {
            try
            {
                var teams = await _teamService.GetAllTeams();

                if (teams.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(teams);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the team by id
        /// </summary>
        /// <param name="id">Team id</param>
        /// <returns>Returns the details of the team with id <paramref name="id"/></returns>
        /// <response code="200">Team found</response>
        /// <response code="404">Team not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetTeamById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamById(int id)
        {
            try
            {
                // Check if team exists
                var foundTeam = await _teamService.GetTeamById(id);

                if (foundTeam == null)
                {
                    return StatusCode(404, "Team not found");
                }

                return Ok(foundTeam);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the team by code
        /// </summary>
        /// <param name="teamCode">Team code</param>
        /// <returns>Returns the details of the team with code <paramref name="teamCode"/></returns>
        /// <response code="200">Team found</response>
        /// <response code="404">Team not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("code/{teamCode}", Name = "GetTeamByCode")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamByCode(Guid teamCode)
        {
            try
            {
                // Check if team exists
                var foundTeam = await _teamService.GetTeamByCode(teamCode);

                if (foundTeam == null)
                {
                    return StatusCode(404, "Team not found");
                }

                return Ok(foundTeam);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing team
        /// </summary>
        /// <param name="id">The id of the team that will be updated</param>
        /// <param name="team">New team details</param>
        /// <returns>Returns the details of the team with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/teams
        ///     {
        ///         "organization": "XYZ Corporation",
        ///         "firstName": "Alice",
        ///         "lastName": "Smith",
        ///         "businessType": "Finance",
        ///         "businessAddress": "456 Elm Avenue"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the team</response>
        /// <response code="404">Team not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateTeam")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamUpdationDto team)
        {
            try
            {
                // Check if team exists
                var foundTeam = await _teamService.GetTeamById(id);

                if (foundTeam == null)
                {
                    return StatusCode(404, "Team not found");
                }

                // Update the team
                await _teamService.UpdateTeam(id, team);
                return Ok("Team updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing team
        /// </summary>
        /// <param name="id">The id of the team that will be deleted</param>
        /// <response code="200">Successfully deleted the team</response>
        /// <response code="404">Team not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteTeam")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                // Check if team exists
                var foundTeam = await _teamService.GetTeamById(id);

                if (foundTeam == null)
                {
                    return StatusCode(404, "Team not found");
                }

                await _teamService.DeleteTeam(id);
                return Ok("Team deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}