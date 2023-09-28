using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/employerassessmentitems")]
    [ApiController]
    public class EmployerAssessmentItemController : ControllerBase
    {
        private readonly IEmployerAssessmentItemService _itemService;
        private readonly ILogger<EmployerAssessmentItemController> _logger;

        public EmployerAssessmentItemController(IEmployerAssessmentItemService itemService, ILogger<EmployerAssessmentItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new employer assessment item
        /// </summary>
        /// <param name="item">Employer assessment item details</param>
        /// <returns>Returns the newly created employer assessment item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/employerassessmentitems
        ///     {
        ///         "question": "What can you say about the overall performance of your work?",
        ///         "questionType": "Short Answer",
        ///         "weight": 100,
        ///         "target": 100,
        ///         "required": "true",
        ///         "employerAssessmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new employer assessment item</response>
        /// <response code="400">Employer assessment item details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateEmployerAssessmentItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployerAssessmentItemCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployerAssessmentItem([FromBody] EmployerAssessmentItemCreationDto item)
        {
            try
            {
                // Create a new employer assessment item
                var newItem = await _itemService.CreateEmployerAssessmentItem(item);

                return CreatedAtRoute("GetEmployerAssessmentItemById", new { id = newItem.Id }, newItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all employer assessment items
        /// </summary>
        /// <returns>Returns all employer assessment items</returns>
        /// <response code="200">Employer assessment items found</response>
        /// <response code="204">No employer assessment items found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllEmployerAssessmentItems")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployerAssessmentItems()
        {
            try
            {
                var items = await _itemService.GetAllEmployerAssessmentItems();

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
        /// Gets the employer assessment item by id
        /// </summary>
        /// <param name="id">Employer assessment item id</param>
        /// <returns>Returns the details of a employer assessment item with id <paramref name="id"/></returns>
        /// <response code="200">Employer assessment item found</response>
        /// <response code="404">Employer assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetEmployerAssessmentItemById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployerAssessmentItemById(int id)
        {
            try
            {
                // Check if employer assessment item exists
                var foundItem = await _itemService.GetEmployerAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Employer assessment item not found");
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
        /// Updates an existing employer assessment item
        /// </summary>
        /// <param name="id">The id of the employer assessment item that will be updated</param>
        /// <param name="item">New employer assessment item details</param>
        /// <returns>Returns the details of the employer assessment item with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/employerassessmentitems
        ///     {
        ///         "question": "What steps are you planning on taking to further improve your job performance before your next review?",
        ///         "questionType": "Paragraph",
        ///         "weight": 90,
        ///         "target": 50,
        ///         "required": "false"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the employer assessment item</response>
        /// <response code="404">Employer assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateEmployerAssessmentItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployerAssessmentItemUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployerAssessmentItem(int id, [FromBody] EmployerAssessmentItemUpdationDto item)
        {
            try
            {
                // Check if employer assessment item exists
                var foundItem = await _itemService.GetEmployerAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Employer assessment item not found");
                }

                // Update the employer assessment item
                await _itemService.UpdateEmployerAssessmentItem(id, item);
                return Ok("Employer assessment item updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing employer assessment item
        /// </summary>
        /// <param name="id">The id of the employer assessment item that will be deleted</param>
        /// <response code="200">Successfully deleted the employer assessment item</response>
        /// <response code="404">Employer assessment item not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteEmployerAssessmentItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployerAssessmentItem(int id)
        {
            try
            {
                // Check if employer assessment item exists
                var foundItem = await _itemService.GetEmployerAssessmentItemById(id);

                if (foundItem == null)
                {
                    return StatusCode(404, "Employer assessment item not found");
                }

                await _itemService.DeleteEmployerAssessmentItem(id);
                return Ok("Employer assessment item deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}