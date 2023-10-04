﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/schedulers")]
    [ApiController]
    public class AssignSchedulerController : ControllerBase
    {
        private readonly IAssignSchedulerService _assignSchedulerService;
        private readonly ILogger<AssignSchedulerController> _logger;

        public AssignSchedulerController(IAssignSchedulerService assignSchedulerService, ILogger<AssignSchedulerController> logger)
        {
            _assignSchedulerService = assignSchedulerService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new scheduler
        /// </summary>
        /// <param name="scheduler">Scheduler details</param>
        /// <returns>Returns the newly created scheduler</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/schedulers
        ///     {
        ///         "assessmentId": 1,
        ///         "employeeId": 1,
        ///         "reminder": "Everyday",
        ///         "occurrence": "Once",
        ///         "dueDate": "Wednesday, October 4, 2023",
        ///         "time": "11:59 PM"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new scheduler</response>
        /// <response code="400">Scheduler details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateAssignScheduler")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AssignSchedulerCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAssignScheduler([FromBody] AssignSchedulerCreationDto scheduler)
        {
            try
            {
                // Create a new scheduler
                var newScheduler = await _assignSchedulerService.CreateAssignScheduler(scheduler);

                return CreatedAtRoute("GetAssignSchedulerById", new { id = newScheduler.Id }, newScheduler);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all schedulers
        /// </summary>
        /// <returns>Returns all schedulers</returns>
        /// <response code="200">Schedulers found</response>
        /// <response code="204">No schedulers found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllAssignSchedulers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAssignSchedulers()
        {
            try
            {
                var schedulers = await _assignSchedulerService.GetAllAssignSchedulers();

                if (schedulers.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(schedulers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the scheduler by id
        /// </summary>
        /// <param name="id">Scheduler id</param>
        /// <returns>Returns the details of the scheduler with id <paramref name="id"/></returns>
        /// <response code="200">Scheduler found</response>
        /// <response code="404">Scheduler not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetAssignSchedulerById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignSchedulerById(int id)
        {
            try
            {
                // Check if scheduler exists
                var foundScheduler = await _assignSchedulerService.GetAssignSchedulerById(id);

                if (foundScheduler == null)
                {
                    return StatusCode(404, "Scheduler not found");
                }

                return Ok(foundScheduler);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the scheduler by assessment id
        /// </summary>
        /// <param name="assessmentId">Assessment id</param>
        /// <returns>Returns the details of the scheduler with assessment id <paramref name="assessmentId"/></returns>
        /// <response code="200">Scheduler found</response>
        /// <response code="404">Scheduler not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("assessments/{assessmentId}", Name = "GetAssignSchedulerByAssessmentId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignSchedulerByAssessmentId(int assessmentId)
        {
            try
            {
                // Check if scheduler exists
                var foundScheduler = await _assignSchedulerService.GetAssignSchedulerByAssessmentId(assessmentId);

                if (foundScheduler == null)
                {
                    return StatusCode(404, "Scheduler not found");
                }

                return Ok(foundScheduler);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the scheduler by employee id
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>Returns the details of the scheduler with employee id <paramref name="employeeId"/></returns>
        /// <response code="200">Scheduler found</response>
        /// <response code="404">Scheduler not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("employees/{employeeId}", Name = "GetAssignSchedulerByEmployeeId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignSchedulerByEmployeeId(int employeeId)
        {
            try
            {
                // Check if scheduler exists
                var foundScheduler = await _assignSchedulerService.GetAssignSchedulerByEmployeeId(employeeId);

                if (foundScheduler == null)
                {
                    return StatusCode(404, "Scheduler not found");
                }

                return Ok(foundScheduler);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing scheduler
        /// </summary>
        /// <param name="id">The id of the scheduler that will be updated</param>
        /// <param name="scheduler">New scheduler details</param>
        /// <returns>Returns the details of the scheduler with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/schedulers
        ///     {
        ///         "assessmentId": 2,
        ///         "employeeId": 2,
        ///         "reminder": "Task Deadline",
        ///         "occurrence": "Weekly",
        ///         "dueDate": "Thursday, October 5, 2023",
        ///         "time": "10:00 PM"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated a scheduler</response>
        /// <response code="404">Scheduler not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateAssignScheduler")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AssignSchedulerUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAssignScheduler(int id, [FromBody] AssignSchedulerUpdationDto scheduler)
        {
            try
            {
                // Check if scheduler exists
                var foundScheduler = await _assignSchedulerService.GetAssignSchedulerById(id);

                if (foundScheduler == null)
                {
                    return StatusCode(404, "Scheduler not found");
                }

                // Update the scheduler
                await _assignSchedulerService.UpdateAssignScheduler(id, scheduler);
                return Ok("Scheduler updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing scheduler
        /// </summary>
        /// <param name="id">The id of the scheduler that will be deleted</param>
        /// <response code="200">Successfully deleted the scheduler</response>
        /// <response code="404">Scheduler not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteAssignScheduler")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAssignScheduler(int id)
        {
            try
            {
                // Check if schduler exists
                var foundScheduler = await _assignSchedulerService.GetAssignSchedulerById(id);

                if (foundScheduler == null)
                {
                    return StatusCode(404, "Scheduler not found");
                }

                await _assignSchedulerService.DeleteAssignScheduler(id);
                return Ok("Scheduler deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}