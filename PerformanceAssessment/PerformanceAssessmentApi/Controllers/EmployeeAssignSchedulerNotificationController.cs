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
        ///         "assessmentId": 1,
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
        public async Task<IActionResult> CreateEmployeeNotification([FromBody] EmployeeAssignSchedulerNotificationCreationDto employeeNotification)
        {
            try
            {
                // Create a new employee notification
                var newEmployeeNotification = await _employeeNotificationService.CreateEmployeeNotification(employeeNotification);

                return CreatedAtRoute("GetEmployeeNotificationById", new { id = newEmployeeNotification.Id }, newEmployeeNotification);
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
        public async Task<IActionResult> GetAllEmployeeNotifications()
        {
            try
            {
                var employeeNotifications = await _employeeNotificationService.GetAllEmployeeNotifications();

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
        public async Task<IActionResult> GetEmployeeNotificationById(int id)
        {
            try
            {
                // Check if employee notification exists
                var foundEmployeeNotification = await _employeeNotificationService.GetEmployeeNotificationById(id);

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
        [HttpGet("employees/{employeeId}", Name = "GetEmployeeAssignSchedulerNotificationByEmployeeId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeNotificationByEmployeeId(int employeeId)
        {
            try
            {
                // Check if employee notification exists
                var foundEmployeeNotification = await _employeeNotificationService.GetAllEmployeeNotificationsByEmployeeId(employeeId);

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
    }
}