using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/employee-assignscheduler-notifications")]
    [ApiController]
    public class EmployeeAssignSchedulerNotificationController : ControllerBase
    {
        private readonly IEmployeeAssignSchedulerNotificationService _employeeNotificationService;
        private readonly ILogger<EmployeeAssignSchedulerNotificationController> _logger;

        public EmployeeAssignSchedulerNotificationController(IEmployeeAssignSchedulerNotificationService employeeNotificationService, ILogger<EmployeeAssignSchedulerNotificationController> logger)
        {
            _employeeNotificationService = employeeNotificationService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new employee assign scheduler notification
        /// </summary>
        /// <param name="employeeNotification">Employee notification details</param>
        /// <returns>Returns the newly created employee notification</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/employee-assignscheduler-notifications
        ///     {
        ///         "employeeId": 1,
        ///         "assessmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new employee notification</response>
        /// <response code="400">Employee notification details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateEmployeeAssignSchedulerNotification")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployeeAssignSchedulerNotificationCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployeeAssignSchedulerNotification([FromBody] EmployeeAssignSchedulerNotificationCreationDto employeeNotification)
        {
            try
            {
                // Create a new employee notification
                var newEmployeeNotification = await _employeeNotificationService.CreateEmployeeAssignSchedulerNotification(employeeNotification);

                return CreatedAtRoute("GetEmployeeAssignSchedulerNotificationById", new { id = newEmployeeNotification.Id }, newEmployeeNotification);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all employee notifications
        /// </summary>
        /// <returns>Returns all employee notifications</returns>
        /// <response code="200">Employee notifications found</response>
        /// <response code="204">No employee notifications found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllEmployeeAssignSchedulerNotifications")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployeeAssignSchedulerNotifications()
        {
            try
            {
                var employeeNotifications = await _employeeNotificationService.GetAllEmployeeAssignSchedulerNotifications();

                if (employeeNotifications.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(employeeNotifications);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the employee notification by id
        /// </summary>
        /// <param name="id">Employee notification id</param>
        /// <returns>Returns the details of an employee notification with id <paramref name="id"/></returns>
        /// <response code="200">Employee notification found</response>
        /// <response code="404">Employee notification not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetEmployeeAssignSchedulerNotificationById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeAssignSchedulerNotificationById(int id)
        {
            try
            {
                // Check if employee notification exists
                var foundEmployeeNotification = await _employeeNotificationService.GetEmployeeAssignSchedulerNotificationById(id);

                if (foundEmployeeNotification == null)
                {
                    return StatusCode(404, "Employee notification not found");
                }

                return Ok(foundEmployeeNotification);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the employee notification by employee id
        /// </summary>
        /// <param name="employeeId">Employee notification id</param>
        /// <returns>Returns the details of an employee notification with id <paramref name="employeeId"/></returns>
        /// <response code="200">Employee notification found</response>
        /// <response code="404">Employee notification not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("employees/{employeeId}", Name = "GetEmployeeAssignSchedulerNotificationsByEmployeeId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeAssignSchedulerNotificationsByEmployeeId(int employeeId)
        {
            try
            {
                // Check if employee notification exists
                var foundEmployeeNotification = await _employeeNotificationService.GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId);

                if (foundEmployeeNotification == null)
                {
                    return StatusCode(404, "Employee notification not found");
                }

                return Ok(foundEmployeeNotification);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Marks the associated employee assign scheduler notification as read
        /// </summary>
        /// <param name="id">The id of the employee assign scheduler notification</param>
        /// <response code="200">Successfully marked the notification as read</response>
        /// <response code="404">Employee assign scheduler notification not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("{id}/mark-as-read", Name = "MarkEmployeeAssignSchedulerNotificationAsRead")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkEmployeeAssignSchedulerNotificationAsRead(int id)
        {
            try
            {
                // Check if the notification exists
                var foundNotification = await _employeeNotificationService.GetEmployeeAssignSchedulerNotificationById(id);

                if (foundNotification == null)
                {
                    return StatusCode(404, "Notification not found");
                }

                await _employeeNotificationService.MarkEmployeeAssignSchedulerNotificationAsRead(id);
                return Ok("Notification has been marked as read successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}