using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="item">Item details</param>
        /// <returns>Returns the newly created item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/items
        ///     {
        ///         "question": "What can you say about the overall performance of your work?",
        ///         "questionType": "Short Answer",
        ///         "weight": 100,
        ///         "required": "true",
        ///         "assessmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new item</response>
        /// <response code="400">Item details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreationDto item)
        {
            try
            {
                // Create a new item
                var newItem = await _itemService.CreateItem(item);

                return CreatedAtRoute("GetItemById", new { id = newItem.Id }, newItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>Returns all items</returns>
        /// <response code="200">Items found</response>
        /// <response code="204">No items found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllItems")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _itemService.GetAllItems();

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
        /// Gets the item by id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Returns the details of an item with id <paramref name="id"/></returns>
        /// <response code="200">Item found</response>
        /// <response code="404">Item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetItemById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                // Check if item exists
                var foundItem = await _itemService.GetItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Item not found");
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
        /// Updates an existing item
        /// </summary>
        /// <param name="id">The id of the item that will be updated</param>
        /// <param name="item">New item details</param>
        /// <returns>Returns the details of the item with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/items
        ///     {
        ///         "question": "What steps are you planning on taking to further improve your job performance before your next review?",
        ///         "questionType": "Paragraph",
        ///         "weight": 90,
        ///         "required": "false"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the item</response>
        /// <response code="404">Item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemUpdationDto item)
        {
            try
            {
                // Check if item exists
                var foundItem = await _itemService.GetItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Item not found");
                }

                // Update the item
                await _itemService.UpdateItem(id, item);
                return Ok("Item updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing item
        /// </summary>
        /// <param name="id">The id of the item that will be deleted</param>
        /// <response code="200">Successfully deleted the item</response>
        /// <response code="404">Item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                // Check if item exists
                var foundItem = await _itemService.GetItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Item not found");
                }

                await _itemService.DeleteItem(id);
                return Ok("Item deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}