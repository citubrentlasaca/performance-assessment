using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/employerassessmentchoices")]
    [ApiController]
    public class EmployerAssessmentChoiceController : ControllerBase
    {
        private readonly IEmployerAssessmentChoiceService _choiceService;
        private readonly ILogger<EmployerAssessmentChoiceController> _logger;

        public EmployerAssessmentChoiceController(IEmployerAssessmentChoiceService choiceService, ILogger<EmployerAssessmentChoiceController> logger)
        {
            _choiceService = choiceService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new employer assessment choice
        /// </summary>
        /// <param name="choice">Employer assessment choice details</param>
        /// <returns>Returns the newly created employer assessment choice</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/employerassessmentchoices
        ///     {
        ///         "choiceValue": "Evaluation",
        ///         "employerAssessmentItemId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new employer assessment choice</response>
        /// <response code="400">Employer assessment choice details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateEmployerAssessmentChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployerAssessmentChoiceCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployerAssessmentChoice([FromBody] EmployerAssessmentChoiceCreationDto choice)
        {
            try
            {
                // Create a new employer assessment choice
                var newChoice = await _choiceService.CreateEmployerAssessmentChoice(choice);

                return CreatedAtRoute("GetEmployerAssessmentChoiceById", new { id = newChoice.Id }, newChoice);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all employer assessment choices
        /// </summary>
        /// <returns>Returns all employer assessment choices</returns>
        /// <response code="200">Employer assessment choices found</response>
        /// <response code="204">No employer assessment choices found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllEmployerAssessmentChoices")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployerAssessmentChoices()
        {
            try
            {
                var choices = await _choiceService.GetAllEmployerAssessmentChoices();

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
        /// Gets the employer assessment choice by id
        /// </summary>
        /// <param name="id">Employer assessment choice id</param>
        /// <returns>Returns the details of a employer assessment choice with id <paramref name="id"/></returns>
        /// <response code="200">Employer assessment choice found</response>
        /// <response code="404">Employer assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetEmployerAssessmentChoiceById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployerAssessmentChoiceById(int id)
        {
            try
            {
                // Check if employer assessment choice exists
                var foundChoice = await _choiceService.GetEmployerAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Employer assessment choice not found");
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
        /// Updates an existing employer assessment choice
        /// </summary>
        /// <param name="id">The id of the employer assessment choice that will be updated</param>
        /// <param name="choice">New employer assessment choice details</param>
        /// <returns>Returns the details of the employer assessment choice with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/employerassessmentchoices
        ///     {
        ///         "choiceValue": "Analytics"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the employer assessment choice</response>
        /// <response code="404">Employer assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateEmployerAssessmentChoice")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployerAssessmentChoiceUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployerAssessmentChoice(int id, [FromBody] EmployerAssessmentChoiceUpdationDto choice)
        {
            try
            {
                // Check if employer assessment choice exists
                var foundChoice = await _choiceService.GetEmployerAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Employer assessment choice not found");
                }

                // Update the choice
                await _choiceService.UpdateEmployerAssessmentChoice(id, choice);
                return Ok("Employer assessment choice updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing employer assessment choice
        /// </summary>
        /// <param name="id">The id of the employer assessment choice that will be deleted</param>
        /// <response code="200">Successfully deleted the employer assessment choice</response>
        /// <response code="404">Employer assessment choice not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteEmployerAssessmentChoice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployerAssessmentChoice(int id)
        {
            try
            {
                // Check if employer assessment choice exists
                var foundChoice = await _choiceService.GetEmployerAssessmentChoiceById(id);

                if (foundChoice == null)
                {
                    return StatusCode(404, "Employer assessment choice not found");
                }

                await _choiceService.DeleteEmployerAssessmentChoice(id);
                return Ok("Employer assessment choice deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}