using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/peerassessmentitems")]
    [ApiController]
    public class PeerAssessmentItemController : ControllerBase
    {
        private readonly IPeerAssessmentItemService _itemService;
        private readonly ILogger<PeerAssessmentItemController> _logger;

        public PeerAssessmentItemController(IPeerAssessmentItemService itemService, ILogger<PeerAssessmentItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new peer assessment item
        /// </summary>
        /// <param name="item">Peer assessment item details</param>
        /// <returns>Returns the newly created peer assessment item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/peerassessmentitems
        ///     {
        ///         "question": "What can you say about the overall performance of your work?",
        ///         "questionType": "Short Answer",
        ///         "weight": 100,
        ///         "target": 100,
        ///         "required": "true",
        ///         "peerAssessmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new peer assessment item</response>
        /// <response code="400">Peer assessment item details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreatePeerAssessmentItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PeerAssessmentItemCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePeerAssessmentItem([FromBody] PeerAssessmentItemCreationDto item)
        {
            try
            {
                // Create a new peer assessment item
                var newItem = await _itemService.CreatePeerAssessmentItem(item);

                return CreatedAtRoute("GetPeerAssessmentItemById", new { id = newItem.Id }, newItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all peer assessment items
        /// </summary>
        /// <returns>Returns all peer assessment items</returns>
        /// <response code="200">Peer assessment items found</response>
        /// <response code="204">No peer assessment items found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllPeerAssessmentItems")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPeerAssessmentItems()
        {
            try
            {
                var items = await _itemService.GetAllPeerAssessmentItems();

                if (items.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the peer assessment item by id
        /// </summary>
        /// <param name="id">Peer assessment item id</param>
        /// <returns>Returns the details of a peer assessment item with id <paramref name="id"/></returns>
        /// <response code="200">Peer assessment item found</response>
        /// <response code="404">Peer assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetPeerAssessmentItemById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeerAssessmentItemById(int id)
        {
            try
            {
                // Check if peer assessment item exists
                var foundItem = await _itemService.GetPeerAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Peer assessment item not found");
                }

                return Ok(foundItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing peer assessment item
        /// </summary>
        /// <param name="id">The id of the peer assessment item that will be updated</param>
        /// <param name="item">New peer assessment item details</param>
        /// <returns>Returns the details of the peer assessment item with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/peerassessmentitems
        ///     {
        ///         "question": "What steps are you planning on taking to further improve your job performance before your next review?",
        ///         "questionType": "Paragraph",
        ///         "weight": 90,
        ///         "target": 50,
        ///         "required": "false"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the peer assessment item</response>
        /// <response code="404">Peer assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdatePeerAssessmentItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PeerAssessmentItemUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePeerAssessmentItem(int id, [FromBody] PeerAssessmentItemUpdationDto item)
        {
            try
            {
                // Check if peer assessment item exists
                var foundItem = await _itemService.GetPeerAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Peer assessment item not found");
                }

                // Update the peer assessment item
                await _itemService.UpdatePeerAssessmentItem(id, item);
                return Ok("Peer assessment item updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing peer assessment item
        /// </summary>
        /// <param name="id">The id of the peer assessment item that will be deleted</param>
        /// <response code="200">Successfully deleted the peer assessment item</response>
        /// <response code="404">Peer assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeletePeerAssessmentItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePeerAssessmentItem(int id)
        {
            try
            {
                // Check if peer assessment item exists
                var foundItem = await _itemService.GetPeerAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Peer assessment item not found");
                }

                await _itemService.DeletePeerAssessmentItem(id);
                return Ok("Peer assessment item deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}