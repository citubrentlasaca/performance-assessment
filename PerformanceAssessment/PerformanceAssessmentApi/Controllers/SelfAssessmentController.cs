using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/selfassessments")]
    [ApiController]
    public class SelfAssessmentController : ControllerBase
    {
        private readonly ISelfAssessmentService _assessmentService;
        private readonly ISelfAssessmentItemService _itemService;
        private readonly ILogger<SelfAssessmentController> _logger;

        public SelfAssessmentController(ISelfAssessmentService assessmentService, ISelfAssessmentItemService itemService, ILogger<SelfAssessmentController> logger)
        {
            _assessmentService = assessmentService;
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new self assessment
        /// </summary>
        /// <param name="assessment">Self assessment details</param>
        /// <returns>Returns the newly created self assessment</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/selfassessments
        ///     {
        ///         "title": "Software Engineering 1"
        ///         "description": "SPMP"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new self assessment</response>
        /// <response code="400">Self assessment details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateSelfAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SelfAssessmentCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSelfAssessment([FromBody] SelfAssessmentCreationDto assessment)
        {
            try
            {
                // Create a new assessment
                var newAssessment = await _assessmentService.CreateSelfAssessment(assessment);

                return CreatedAtRoute("GetSelfAssessmentById", new { id = newAssessment.Id }, newAssessment);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all self assessments
        /// </summary>
        /// <returns>Returns all self assessments</returns>
        /// <response code="200">Self assessments found</response>
        /// <response code="204">No self assessments found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllSelfAssessments")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSelfAssessments()
        {
            try
            {
                var assessments = await _assessmentService.GetAllSelfAssessments();

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
        /// Gets the self assessment by id
        /// </summary>
        /// <param name="id">Self assessment id</param>
        /// <returns>Returns the details of a self assessment with id <paramref name="id"/></returns>
        /// <response code="200">Self assessment found</response>
        /// <response code="404">Self assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetSelfAssessmentById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSelfAssessmentById(int id)
        {
            try
            {
                // Check if self assessment exists
                var foundAssessment = await _assessmentService.GetSelfAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Self assessment not found");
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
        /// Updates an existing self assessment
        /// </summary>
        /// <param name="id">The id of the self assessment that will be updated</param>
        /// <param name="assessment">New self assessment details</param>
        /// <returns>Returns the details of self assessment with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/selfassessments
        ///     {
        ///         "title": "Programming Languages"
        ///         "description": "History and Uses Of The Programming Languages"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the self assessment</response>
        /// <response code="404">Self assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateSelfAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SelfAssessmentUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSelfAssessment(int id, [FromBody] SelfAssessmentUpdationDto assessment)
        {
            try
            {
                // Check if self assessment exists
                var foundAssessment = await _assessmentService.GetSelfAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Self assessment not found");
                }

                // Update the self assessment
                await _assessmentService.UpdateSelfAssessment(id, assessment);
                return Ok("Self assessment updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing self assessment
        /// </summary>
        /// <param name="id">The id of the self assessment that will be deleted</param>
        /// <response code="200">Successfully deleted the self assessment</response>
        /// <response code="404">Self assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteSelfAssessment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSelfAssessment(int id)
        {
            try
            {
                // Check if self assessment exists
                var foundAssessment = await _assessmentService.GetSelfAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Self assessment not found");
                }

                await _assessmentService.DeleteSelfAssessment(id);
                return Ok("Self assessment deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all items of a self assessment
        /// </summary>
        /// <param name="id">Self assessment id</param>
        /// <returns>Returns the details of items with selfAssessmentid <paramref name="id"/></returns>
        /// <response code="200">Self assessment found</response>
        /// <response code="404">Self asssessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/items", Name = "GetSelfAssessmentItemsById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSelfAssessmentItemsById(int id)
        {
            try
            {
                // Check if a self assessment has an item or not
                var foundItems = await _itemService.GetSelfAssessmentItemById(id);

                if (foundItems == null)
                {
                    return StatusCode(404, "Item/s not found");
                }

                // Check if self assessment exists
                var foundAssessmentItems = await _assessmentService.GetSelfAssessmentItemsById(id);

                if (foundAssessmentItems == null)
                {
                    return StatusCode(404, "Self assessment not found");
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