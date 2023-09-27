using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/selfassessmentitems")]
    [ApiController]
    public class SelfAssessmentItemController : ControllerBase
    {
        private readonly ISelfAssessmentItemService _itemService;
        private readonly ILogger<SelfAssessmentItemController> _logger;

        public SelfAssessmentItemController(ISelfAssessmentItemService itemService, ILogger<SelfAssessmentItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new self assessment item
        /// </summary>
        /// <param name="item">Self assessment item details</param>
        /// <returns>Returns the newly created self assessment item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/selfassessmentitems
        ///     {
        ///         "question": "What can you say about the overall performance of your work?",
        ///         "questionType": "Short Answer",
        ///         "weight": 100,
        ///         "target": 100,
        ///         "required": "true",
        ///         "selfAssessmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new self assessment item</response>
        /// <response code="400">Self assessment item details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateSelfAssessmentItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SelfAssessmentItemCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSelfAssessmentItem([FromBody] SelfAssessmentItemCreationDto item)
        {
            try
            {
                // Create a new self assessment item
                var newItem = await _itemService.CreateSelfAssessmentItem(item);

                return CreatedAtRoute("GetSelfAssessmentItemById", new { id = newItem.Id }, newItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all self assessment items
        /// </summary>
        /// <returns>Returns all self assessment items</returns>
        /// <response code="200">Self assessment items found</response>
        /// <response code="204">No self assessment items found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllSelfAssessmentItems")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSelfAssessmentItems()
        {
            try
            {
                var items = await _itemService.GetAllSelfAssessmentItems();

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
        /// Gets the self assessment item by id
        /// </summary>
        /// <param name="id">Self assessment item id</param>
        /// <returns>Returns the details of a self assessment item with id <paramref name="id"/></returns>
        /// <response code="200">Self assessment item found</response>
        /// <response code="404">Self assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetSelfAssessmentItemById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSelfAssessmentItemById(int id)
        {
            try
            {
                // Check if self assessment item exists
                var foundItem = await _itemService.GetSelfAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Self assessment item not found");
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
        /// Updates an existing self assessment item
        /// </summary>
        /// <param name="id">The id of the self assessment item that will be updated</param>
        /// <param name="item">New self assessment item details</param>
        /// <returns>Returns the details of the self assessment item with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/selfassessmentitems
        ///     {
        ///         "question": "What steps are you planning on taking to further improve your job performance before your next review?",
        ///         "questionType": "Paragraph",
        ///         "weight": 90,
        ///         "target": 50,
        ///         "required": "false"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the self assessment item</response>
        /// <response code="404">Self assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateSelfAssessmentItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SelfAssessmentItemUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSelfAssessmentItem(int id, [FromBody] SelfAssessmentItemUpdationDto item)
        {
            try
            {
                // Check if self assessment item exists
                var foundItem = await _itemService.GetSelfAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Self assessment item not found");
                }

                // Update the self assessment item
                await _itemService.UpdateSelfAssessmentItem(id, item);
                return Ok("Self assessment item updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing self assessment item
        /// </summary>
        /// <param name="id">The id of the self assessment item that will be deleted</param>
        /// <response code="200">Successfully deleted the self assessment item</response>
        /// <response code="404">Self assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteSelfAssessmentItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSelfAssessmentItem(int id)
        {
            try
            {
                // Check if self assessment item exists
                var foundItem = await _itemService.GetSelfAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Self assessment item not found");
                }

                await _itemService.DeleteSelfAssessmentItem(id);
                return Ok("Self assessment item deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}