using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/choices")]
    [ApiController]
    public class ChoiceController : ControllerBase
    {
        private readonly IChoiceService _choiceService;
        private readonly ILogger<ChoiceController> _logger;

        public ChoiceController(IChoiceService choiceService, ILogger<ChoiceController> logger)
        {
            _choiceService = choiceService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new choice
        /// </summary>
        /// <param name="choice">Choice details</param>
        /// <returns>Returns the newly created choice</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/choices
        ///     {
        ///         "choiceValue": "Evaluation",
        ///         "weight": 50,
        ///         "itemId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new choice</response>
        /// <response code="400">Choice details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ChoiceCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateChoice([FromBody] ChoiceCreationDto choice)
        {
            try
            {
                // Create a new choice
                var newChoice = await _choiceService.CreateChoice(choice);

                return CreatedAtRoute("GetChoiceById", new { id = newChoice.Id }, newChoice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all choices
        /// </summary>
        /// <returns>Returns all choices</returns>
        /// <response code="200">Choices found</response>
        /// <response code="204">No choices found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllChoices")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllChoices()
        {
            try
            {
                var choices = await _choiceService.GetAllChoices();

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
        /// Gets the choice by id
        /// </summary>
        /// <param name="id">Choice id</param>
        /// <returns>Returns the details of a choice with id <paramref name="id"/></returns>
        /// <response code="200">Choice found</response>
        /// <response code="404">Choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetChoiceById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChoiceById(int id)
        {
            try
            {
                // Check if choice exists
                var foundChoice = await _choiceService.GetChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Choice not found");
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
        /// Updates an existing choice
        /// </summary>
        /// <param name="id">The id of the choice that will be updated</param>
        /// <param name="choice">New choice details</param>
        /// <returns>Returns the details of the choice with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/choices
        ///     {
        ///         "choiceValue": "Analytics",
        ///         "weight": 30
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the choice</response>
        /// <response code="404">Choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ChoiceUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateChoice(int id, [FromBody] ChoiceUpdationDto choice)
        {
            try
            {
                // Check if choice exists
                var foundChoice = await _choiceService.GetChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Choice not found");
                }

                // Update the choice
                await _choiceService.UpdateChoice(id, choice);
                return Ok("Choice updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing choice
        /// </summary>
        /// <param name="id">The id of the choice that will be deleted</param>
        /// <response code="200">Successfully deleted the choice</response>
        /// <response code="404">Choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteChoice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            try
            {
                // Check if choice exists
                var foundChoice = await _choiceService.GetChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Choice not found");
                }

                await _choiceService.DeleteChoice(id);
                return Ok("Choice deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}