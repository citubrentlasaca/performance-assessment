using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/selfassessmentchoices")]
    [ApiController]
    public class SelfAssessmentChoiceController : ControllerBase
    {
        private readonly ISelfAssessmentChoiceService _choiceService;
        private readonly ILogger<SelfAssessmentChoiceController> _logger;

        public SelfAssessmentChoiceController(ISelfAssessmentChoiceService choiceService, ILogger<SelfAssessmentChoiceController> logger)
        {
            _choiceService = choiceService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new self assessment choice
        /// </summary>
        /// <param name="choice">Self assessment choice details</param>
        /// <returns>Returns the newly created self assessment choice</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/selfassessmentchoices
        ///     {
        ///         "choiceValue": "Evaluation",
        ///         "selfAssessmentItemId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new self assessment choice</response>
        /// <response code="400">Self assessment choice details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateSelfAssessmentChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SelfAssessmentChoiceCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSelfAssessmentChoice([FromBody] SelfAssessmentChoiceCreationDto choice)
        {
            try
            {
                // Create a new self assessment choice
                var newChoice = await _choiceService.CreateSelfAssessmentChoice(choice);

                return CreatedAtRoute("GetSelfAssessmentChoiceById", new { id = newChoice.Id }, newChoice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all self assessment choices
        /// </summary>
        /// <returns>Returns all self assessment choices</returns>
        /// <response code="200">Self assessment choices found</response>
        /// <response code="204">No self assessment choices found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllSelfAssessmentChoices")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSelfAssessmentChoices()
        {
            try
            {
                var choices = await _choiceService.GetAllSelfAssessmentChoices();

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
        /// Gets the self assessment choice by id
        /// </summary>
        /// <param name="id">Self assessment choice id</param>
        /// <returns>Returns the details of a self assessment choice with id <paramref name="id"/></returns>
        /// <response code="200">Self assessment choice found</response>
        /// <response code="404">Self assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetSelfAssessmentChoiceById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSelfAssessmentChoiceById(int id)
        {
            try
            {
                // Check if self assessment choice exists
                var foundChoice = await _choiceService.GetSelfAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Self assessment choice not found");
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
        /// Updates an existing self assessment choice
        /// </summary>
        /// <param name="id">The id of the self assessment choice that will be updated</param>
        /// <param name="choice">New self assessment choice details</param>
        /// <returns>Returns the details of the self assessment choice with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/selfassessmentchoices
        ///     {
        ///         "choiceValue": "Analytics"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the self assessment choice</response>
        /// <response code="404">Self assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateSelfAssessmentChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ChoiceUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSelfAssessmentChoice(int id, [FromBody] SelfAssessmentChoiceUpdationDto choice)
        {
            try
            {
                // Check if self assessment choice exists
                var foundChoice = await _choiceService.GetSelfAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Self assessment choice not found");
                }

                // Update the choice
                await _choiceService.UpdateSelfAssessmentChoice(id, choice);
                return Ok("Self assessment choice updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing self assessment choice
        /// </summary>
        /// <param name="id">The id of the self assessment choice that will be deleted</param>
        /// <response code="200">Successfully deleted the self assessment choice</response>
        /// <response code="404">Self assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteSelfAssessmentChoice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSelfAssessmentChoice(int id)
        {
            try
            {
                // Check if self assessment choice exists
                var foundChoice = await _choiceService.GetSelfAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Self assessment choice not found");
                }

                await _choiceService.DeleteSelfAssessmentChoice(id);
                return Ok("Self assessment choice deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}