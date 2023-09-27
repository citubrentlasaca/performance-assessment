using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/employerassessments")]
    [ApiController]
    public class EmployerAssessmentController : ControllerBase
    {
        private readonly IEmployerAssessmentService _assessmentService;
        private readonly IEmployerAssessmentItemService _itemService;
        private readonly ILogger<EmployerAssessmentController> _logger;

        public EmployerAssessmentController(IEmployerAssessmentService assessmentService, IEmployerAssessmentItemService itemService, ILogger<EmployerAssessmentController> logger)
        {
            _assessmentService = assessmentService;
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new employer assessment
        /// </summary>
        /// <param name="assessment">Employer assessment details</param>
        /// <returns>Returns the newly created employer assessment</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/employerassessments
        ///     {
        ///         "title": "Software Engineering 1"
        ///         "description": "SPMP"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new employer assessment</response>
        /// <response code="400">Employer assessment details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateEmployerAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployerAssessmentCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployerAssessment([FromBody] EmployerAssessmentCreationDto assessment)
        {
            try
            {
                // Create a new assessment
                var newAssessment = await _assessmentService.CreateEmployerAssessment(assessment);

                return CreatedAtRoute("GetEmployerAssessmentById", new { id = newAssessment.Id }, newAssessment);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all employer assessments
        /// </summary>
        /// <returns>Returns all employer assessments</returns>
        /// <response code="200">Employer assessments found</response>
        /// <response code="204">No employer assessments found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllEmployerAssessments")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployerAssessments()
        {
            try
            {
                var assessments = await _assessmentService.GetAllEmployerAssessments();

                if (assessments.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(assessments);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the employer assessment by id
        /// </summary>
        /// <param name="id">Employer assessment id</param>
        /// <returns>Returns the details of a employer assessment with id <paramref name="id"/></returns>
        /// <response code="200">Employer assessment found</response>
        /// <response code="404">Employer assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetEmployerAssessmentById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployerAssessmentById(int id)
        {
            try
            {
                // Check if employer assessment exists
                var foundAssessment = await _assessmentService.GetEmployerAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Employer assessment not found");
                }

                return Ok(foundAssessment);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing employer assessment
        /// </summary>
        /// <param name="id">The id of the employer assessment that will be updated</param>
        /// <param name="assessment">New employer assessment details</param>
        /// <returns>Returns the details of employer assessment with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/employerassessments
        ///     {
        ///         "title": "Programming Languages"
        ///         "description": "History and Uses Of The Programming Languages"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the employer assessment</response>
        /// <response code="404">Employer assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateEmployerAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployerAssessmentUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployerAssessment(int id, [FromBody] EmployerAssessmentUpdationDto assessment)
        {
            try
            {
                // Check if employer assessment exists
                var foundAssessment = await _assessmentService.GetEmployerAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Employer assessment not found");
                }

                // Update the employer assessment
                await _assessmentService.UpdateEmployerAssessment(id, assessment);
                return Ok("Employer assessment updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing employer assessment
        /// </summary>
        /// <param name="id">The id of the employer assessment that will be deleted</param>
        /// <response code="200">Successfully deleted the employer assessment</response>
        /// <response code="404">Employer assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteEmployerAssessment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployerAssessment(int id)
        {
            try
            {
                // Check if employer assessment exists
                var foundAssessment = await _assessmentService.GetEmployerAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Employer assessment not found");
                }

                await _assessmentService.DeleteEmployerAssessment(id);
                return Ok("Employer assessment deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all items of a employer assessment
        /// </summary>
        /// <param name="id">Employer assessment id</param>
        /// <returns>Returns the details of items with employerAssessmentid <paramref name="id"/></returns>
        /// <response code="200">Employer assessment found</response>
        /// <response code="404">Employer asssessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/items", Name = "GetEmployerAssessmentItemsById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployerAssessmentItemsById(int id)
        {
            try
            {
                // Check if a employer assessment has an item or not
                var foundItems = await _itemService.GetEmployerAssessmentItemById(id);

                if (foundItems == null)
                {
                    return StatusCode(404, "Item/s not found");
                }

                // Check if employer assessment exists
                var foundAssessmentItems = await _assessmentService.GetEmployerAssessmentItemsById(id);

                if (foundAssessmentItems == null)
                {
                    return StatusCode(404, "Employer assessment not found");
                }

                return Ok(foundAssessmentItems);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}