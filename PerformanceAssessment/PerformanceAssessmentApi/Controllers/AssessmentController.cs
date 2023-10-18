using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/assessments")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;
        private readonly IItemService _itemService;
        private readonly ILogger<AssessmentController> _logger;

        public AssessmentController(IAssessmentService assessmentService, IItemService itemService, ILogger<AssessmentController> logger)
        {
            _assessmentService = assessmentService;
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new assessment
        /// </summary>
        /// <param name="assessment">Assessment details</param>
        /// <returns>Returns the newly created assessment</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/assessments
        ///     {
        ///         "employeeId": 1,
        ///         "title": "Software Engineering 1",
        ///         "description": "SPMP"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new assessment</response>
        /// <response code="400">Assessment details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AssessmentCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAssessment([FromBody] AssessmentCreationDto assessment)
        {
            try
            {
                // Create a new assessment
                var newAssessment = await _assessmentService.CreateAssessment(assessment);

                return CreatedAtRoute("GetAssessmentById", new { id = newAssessment.Id }, newAssessment);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all assessments
        /// </summary>
        /// <returns>Returns all assessments</returns>
        /// <response code="200">Assessments found</response>
        /// <response code="204">No assessments found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllAssessments")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAssessments()
        {
            try
            {
                var assessments = await _assessmentService.GetAllAssessments();

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
        /// Gets the assessment by id
        /// </summary>
        /// <param name="id">Assessment id</param>
        /// <returns>Returns the details of an assessment with id <paramref name="id"/></returns>
        /// <response code="200">Assessment found</response>
        /// <response code="404">Assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetAssessmentById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssessmentById(int id)
        {
            try
            {
                // Check if assessment exists
                var foundAssessment = await _assessmentService.GetAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Assessment not found");
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
        /// Updates an existing assessment
        /// </summary>
        /// <param name="id">The id of the assessment that will be updated</param>
        /// <param name="assessment">New assessment details</param>
        /// <returns>Returns the details of assessment with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/assessments
        ///     {
        ///         "title": "Programming Languages",
        ///         "description": "History and Uses Of The Programming Languages"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the assessment</response>
        /// <response code="404">Assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AssessmentUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAssessment(int id, [FromBody] AssessmentUpdationDto assessment)
        {
            try
            {
                // Check if assessment exists
                var foundAssessment = await _assessmentService.GetAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Assessment not found");
                }

                // Update the assessment
                await _assessmentService.UpdateAssessment(id, assessment);
                return Ok("Assessment updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing assessment
        /// </summary>
        /// <param name="id">The id of the assessment that will be deleted</param>
        /// <response code="200">Successfully deleted the assessment</response>
        /// <response code="404">Assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteAssessment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAssessment(int id)
        {
            try
            {
                // Check if assessment exists
                var foundAssessment = await _assessmentService.GetAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Assessment not found");
                }

                await _assessmentService.DeleteAssessment(id);
                return Ok("Assessment deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all items of an assessment
        /// </summary>
        /// <param name="id">Assessment id</param>
        /// <returns>Returns the details of items with assessmentid <paramref name="id"/></returns>
        /// <response code="200">Assessment found</response>
        /// <response code="404">Assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/items", Name = "GetAssessmentItemsById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssessmentItemsById(int id)
        {
            try
            {
                // Check if an assessment has an item or not
                var foundItems = await _itemService.GetItemById(id);

                if (foundItems == null)
                {
                    return StatusCode(404, "Item/s not found");
                }

                // Check if assessment exists
                var foundAssessmentItems = await _assessmentService.GetAssessmentItemsById(id);

                if (foundAssessmentItems == null)
                {
                    return StatusCode(404, "Assessment not found");
                }

                return Ok(foundAssessmentItems);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all assessments for a specific employee
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Returns all assessments for the specified employee</returns>
        /// <response code="200">Assessments found</response>
        /// <response code="204">No assessments found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("employee/{employeeId}", Name = "GetAssessmentsByEmployeeId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssessmentsByEmployeeId(int employeeId)
        {
            try
            {
                var assessments = await _assessmentService.GetAssessmentsByEmployeeId(employeeId);

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
    }
}