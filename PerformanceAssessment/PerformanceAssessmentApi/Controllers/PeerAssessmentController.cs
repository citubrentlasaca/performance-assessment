using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/peerassessments")]
    [ApiController]
    public class PeerAssessmentController : ControllerBase
    {
        private readonly IPeerAssessmentService _assessmentService;
        private readonly IPeerAssessmentItemService _itemService;
        private readonly ILogger<PeerAssessmentController> _logger;

        public PeerAssessmentController(IPeerAssessmentService assessmentService, IPeerAssessmentItemService itemService, ILogger<PeerAssessmentController> logger)
        {
            _assessmentService = assessmentService;
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new peer assessment
        /// </summary>
        /// <param name="assessment">Peer assessment details</param>
        /// <returns>Returns the newly created peer assessment</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/peerassessments
        ///     {
        ///         "title": "Software Engineering 1"
        ///         "description": "SPMP"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new peer assessment</response>
        /// <response code="400">Peer assessment details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreatePeerAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PeerAssessmentCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePeerAssessment([FromBody] PeerAssessmentCreationDto assessment)
        {
            try
            {
                // Create a new assessment
                var newAssessment = await _assessmentService.CreatePeerAssessment(assessment);

                return CreatedAtRoute("GetPeerAssessmentById", new { id = newAssessment.Id }, newAssessment);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all peer assessments
        /// </summary>
        /// <returns>Returns all peer assessments</returns>
        /// <response code="200">Peer assessments found</response>
        /// <response code="204">No peer assessments found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllPeerAssessments")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPeerAssessments()
        {
            try
            {
                var assessments = await _assessmentService.GetAllPeerAssessments();

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
        /// Gets the peer assessment by id
        /// </summary>
        /// <param name="id">Peer assessment id</param>
        /// <returns>Returns the details of a peer assessment with id <paramref name="id"/></returns>
        /// <response code="200">Peer assessment found</response>
        /// <response code="404">Peer assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetPeerAssessmentById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeerAssessmentById(int id)
        {
            try
            {
                // Check if peer assessment exists
                var foundAssessment = await _assessmentService.GetPeerAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Peer assessment not found");
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
        /// Updates an existing peer assessment
        /// </summary>
        /// <param name="id">The id of the peer assessment that will be updated</param>
        /// <param name="assessment">New peer assessment details</param>
        /// <returns>Returns the details of peer assessment with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/peerassessments
        ///     {
        ///         "title": "Programming Languages"
        ///         "description": "History and Uses Of The Programming Languages"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the peer assessment</response>
        /// <response code="404">Peer assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdatePeerAssessment")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PeerAssessmentUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePeerAssessment(int id, [FromBody] PeerAssessmentUpdationDto assessment)
        {
            try
            {
                // Check if peer assessment exists
                var foundAssessment = await _assessmentService.GetPeerAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Peer assessment not found");
                }

                // Update the peer assessment
                await _assessmentService.UpdatePeerAssessment(id, assessment);
                return Ok("Peer assessment updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing peer assessment
        /// </summary>
        /// <param name="id">The id of the peer assessment that will be deleted</param>
        /// <response code="200">Successfully deleted the peer assessment</response>
        /// <response code="404">Peer assessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeletePeerAssessment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePeerAssessment(int id)
        {
            try
            {
                // Check if peer assessment exists
                var foundAssessment = await _assessmentService.GetPeerAssessmentById(id);

                if (foundAssessment == null)
                {
                    return StatusCode(404, "Peer assessment not found");
                }

                await _assessmentService.DeletePeerAssessment(id);
                return Ok("Peer assessment deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all items of a peer assessment
        /// </summary>
        /// <param name="id">Peer assessment id</param>
        /// <returns>Returns the details of items with peerAssessmentid <paramref name="id"/></returns>
        /// <response code="200">Peer assessment found</response>
        /// <response code="404">Peer asssessment not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/items", Name = "GetPeerAssessmentItemsById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeerAssessmentItemsById(int id)
        {
            try
            {
                // Check if a peer assessment has an item or not
                var foundItems = await _itemService.GetPeerAssessmentItemById(id);

                if (foundItems == null)
                {
                    return StatusCode(404, "Item/s not found");
                }

                // Check if peer assessment exists
                var foundAssessmentItems = await _assessmentService.GetPeerAssessmentItemsById(id);

                if (foundAssessmentItems == null)
                {
                    return StatusCode(404, "Peer assessment not found");
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