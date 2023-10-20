using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        private readonly ILogger<ResultController> _logger;

        public ResultController(IResultService resultService, ILogger<ResultController> logger)
        {
            _resultService = resultService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a result
        /// </summary>
        /// <param name="result">Result details</param>
        /// <returns>Returns the newly created result</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/results
        ///     {
        ///         "assessmentId": 1,
        ///         "employeeId": 1,
        ///         "score": 50,
        ///         "dateTimeDue": "Monday, October 16, 2023"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a result</response>
        /// <response code="400">Result details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateResult")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResultCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateResult([FromBody] ResultCreationDto result)
        {
            try
            {
                // Create a result
                var newResult = await _resultService.CreateResult(result);

                return CreatedAtRoute("GetResultById", new { id = newResult }, newResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all results
        /// </summary>
        /// <returns>Returns all results</returns>
        /// <response code="200">Results found</response>
        /// <response code="204">No results found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllResults")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllResults()
        {
            try
            {
                var results = await _resultService.GetAllResults();

                if (results.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the result by id
        /// </summary>
        /// <param name="id">Result id</param>
        /// <returns>Returns the details of the result with id <paramref name="id"/></returns>
        /// <response code="200">Result found</response>
        /// <response code="404">Result not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetResultById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetResultById(int id)
        {
            try
            {
                // Check if result exists
                var foundResult = await _resultService.GetResultById(id);

                if (foundResult == null)
                {
                    return StatusCode(404, "Result not found");
                }

                return Ok(foundResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the result by assessment id
        /// </summary>
        /// <param name="resultId">Result id</param>
        /// <returns>Returns the details of the result with assessment id <paramref name="resultId"/></returns>
        /// <response code="200">Result found</response>
        /// <response code="404">Result not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("assessments/{assessmentId}", Name = "GetResultByAssessmentId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetResultByAssessmentId(int assessmentId)
        {
            try
            {
                // Check if result exists
                var foundResult = await _resultService.GetResultByAssessmentId(assessmentId);

                if (foundResult == null)
                {
                    return StatusCode(404, "Result not found");
                }

                return Ok(foundResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the result by employee id
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>Returns the details of the result with employee id <paramref name="employeeId"/></returns>
        /// <response code="200">Result found</response>
        /// <response code="404">Result not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("employees/{employeeId}", Name = "GetResultByEmployeeId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetResultByEmployeeId(int employeeId)
        {
            try
            {
                // Check if result exists
                var foundResult = await _resultService.GetResultByEmployeeId(employeeId);

                if (foundResult == null)
                {
                    return StatusCode(404, "Result not found");
                }

                return Ok(foundResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing result
        /// </summary>
        /// <param name="id">The id of the result that will be updated</param>
        /// <param name="result">New result details</param>
        /// <returns>Returns the details of the result with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/results
        ///     {
        ///         "assessmentId": 2,
        ///         "employeeId": 2,
        ///         "score": 100,
        ///         "dateTimeDue": "Tuesday, October 17, 2023"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated a result</response>
        /// <response code="404">Result not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateResult")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResultUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateResult(int id, [FromBody] ResultUpdationDto result)
        {
            try
            {
                // Check if result exists
                var foundResult = await _resultService.GetResultById(id);

                if (foundResult == null)
                {
                    return StatusCode(404, "Result not found");
                }

                // Update the result
                await _resultService.UpdateResult(id, result);
                return Ok("Result updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing result
        /// </summary>
        /// <param name="id">The id of the result that will be deleted</param>
        /// <response code="200">Successfully deleted the result</response>
        /// <response code="404">Result not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteResult")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteResult(int id)
        {
            try
            {
                // Check if result exists
                var foundResult = await _resultService.GetResultById(id);

                if (foundResult == null)
                {
                    return StatusCode(404, "Result not found");
                }

                await _resultService.DeleteResult(id);
                return Ok("Result deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}