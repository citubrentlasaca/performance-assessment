using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/peerassessmentchoices")]
    [ApiController]
    public class PeerAssessmentChoiceController : ControllerBase
    {
        private readonly IPeerAssessmentChoiceService _choiceService;
        private readonly ILogger<PeerAssessmentChoiceController> _logger;

        public PeerAssessmentChoiceController(IPeerAssessmentChoiceService choiceService, ILogger<PeerAssessmentChoiceController> logger)
        {
            _choiceService = choiceService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new peer assessment choice
        /// </summary>
        /// <param name="choice">Peer assessment choice details</param>
        /// <returns>Returns the newly created peer assessment choice</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/peerassessmentchoices
        ///     {
        ///         "choiceValue": "Evaluation",
        ///         "peerAssessmentItemId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new peer assessment choice</response>
        /// <response code="400">Peer assessment choice details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreatePeerAssessmentChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PeerAssessmentChoiceCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePeerAssessmentChoice([FromBody] PeerAssessmentChoiceCreationDto choice)
        {
            try
            {
                // Create a new peer assessment choice
                var newChoice = await _choiceService.CreatePeerAssessmentChoice(choice);

                return CreatedAtRoute("GetPeerAssessmentChoiceById", new { id = newChoice.Id }, newChoice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all peer assessment choices
        /// </summary>
        /// <returns>Returns all peer assessment choices</returns>
        /// <response code="200">Peer assessment choices found</response>
        /// <response code="204">No peer assessment choices found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllPeerAssessmentChoices")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPeerAssessmentChoices()
        {
            try
            {
                var choices = await _choiceService.GetAllPeerAssessmentChoices();

                if (choices.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(choices);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the peer assessment choice by id
        /// </summary>
        /// <param name="id">Peer assessment choice id</param>
        /// <returns>Returns the details of a peer assessment choice with id <paramref name="id"/></returns>
        /// <response code="200">Peer assessment choice found</response>
        /// <response code="404">Peer assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetPeerAssessmentChoiceById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeerAssessmentChoiceById(int id)
        {
            try
            {
                // Check if peer assessment choice exists
                var foundChoice = await _choiceService.GetPeerAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Peer assessment choice not found");
                }

                return Ok(foundChoice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing peer assessment choice
        /// </summary>
        /// <param name="id">The id of the peer assessment choice that will be updated</param>
        /// <param name="choice">New peer assessment choice details</param>
        /// <returns>Returns the details of the peer assessment choice with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/peerassessmentchoices
        ///     {
        ///         "choiceValue": "Analytics"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the peer assessment choice</response>
        /// <response code="404">Peer assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdatePeerAssessmentChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PeerAssessmentChoiceUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePeerAssessmentChoice(int id, [FromBody] PeerAssessmentChoiceUpdationDto choice)
        {
            try
            {
                // Check if peer assessment choice exists
                var foundChoice = await _choiceService.GetPeerAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Peer assessment choice not found");
                }

                // Update the choice
                await _choiceService.UpdatePeerAssessmentChoice(id, choice);
                return Ok("Peer assessment choice updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing peer assessment choice
        /// </summary>
        /// <param name="id">The id of the peer assessment choice that will be deleted</param>
        /// <response code="200">Successfully deleted the peer assessment choice</response>
        /// <response code="404">Peer assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeletePeerAssessmentChoice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePeerAssessmentChoice(int id)
        {
            try
            {
                // Check if peer assessment choice exists
                var foundChoice = await _choiceService.GetPeerAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Peer assessment choice not found");
                }

                await _choiceService.DeletePeerAssessmentChoice(id);
                return Ok("Peer assessment choice deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}