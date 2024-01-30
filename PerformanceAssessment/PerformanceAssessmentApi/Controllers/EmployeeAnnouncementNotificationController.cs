using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/employee-announcement-notifications")]
    [ApiController]
    public class EmployeeAnnouncementNotificationController : ControllerBase
    {
        private readonly IEmployeeAnnouncementNotificationService _employeeNotificationService;
        private readonly ILogger<EmployeeAnnouncementNotificationController> _logger;

        public EmployeeAnnouncementNotificationController(IEmployeeAnnouncementNotificationService employeeNotificationService, ILogger<EmployeeAnnouncementNotificationController> logger)
        {
            _employeeNotificationService = employeeNotificationService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new employee announcement notification
        /// </summary>
        /// <param name="employeeNotification">Employee notification details</param>
        /// <returns>Returns the newly created employee notification</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/employee-announcement-notifications
        ///     {
        ///         "employeeId": 1,
        ///         "announcementId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new employee notification</response>
        /// <response code="400">Employee notification details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateEmployeeAnnouncementNotification")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployeeAssignSchedulerNotificationCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployeeAnnouncementNotification([FromBody] EmployeeAnnouncementNotificationCreationDto employeeNotification)
        {
            try
            {
                // Create a new employee notification
                var newEmployeeAnnouncementNotification = await _employeeNotificationService.CreateEmployeeAnnouncementNotification(employeeNotification);

                return CreatedAtRoute("GetEmployeeAnnouncementNotificationById", new { id = newEmployeeAnnouncementNotification.Id }, newEmployeeAnnouncementNotification);
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
        [HttpGet(Name = "GetAllEmployeeAnnouncementNotifications")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployeeAnnouncementNotifications()
        {
            try
            {
                var employeeNotifications = await _employeeNotificationService.GetAllEmployeeAnnouncementNotifications();

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
        [HttpGet("{id}", Name = "GetEmployeeAnnouncementNotificationById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeAnnouncementNotificationById(int id)
        {
            try
            {
                // Check if employee notification exists
                var foundEmployeeNotification = await _employeeNotificationService.GetEmployeeAnnouncementNotificationById(id);

                if (foundEmployeeNotification == null)
                {
                    return StatusCode(404, "Employee announcement notification not found");
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
        /// <returns>Returns the details of an employee notification with employee id <paramref name="employeeId"/></returns>
        /// <response code="200">Employee notification found</response>
        /// <response code="404">Employee notification not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("employees/{employeeId}", Name = "GetEmployeeAnnouncementNotificationByEmployeeId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeAnnouncementNotificationByEmployeeId(int employeeId)
        {
            try
            {
                // Check if employee notification exists
                var foundEmployeeNotification = await _employeeNotificationService.GetEmployeeAnnouncementNotificationByEmployeeId(employeeId);

                if (foundEmployeeNotification == null)
                {
                    return StatusCode(404, "Employee announcement notification not found");
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
        /// Marks the associated employee announcement notification as read
        /// </summary>
        /// <param name="id">The id of the employee announcement notification</param>
        /// <response code="200">Successfully marked the notification as read</response>
        /// <response code="404">Employee announcement notification not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("{id}/mark-as-read", Name = "MarkEmployeeAnnouncementNotificationAsRead")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkEmployeeAnnouncementNotificationAsRead(int id)
        {
            try
            {
                // Check if the notification exists
                var foundNotification = await _employeeNotificationService.GetEmployeeAnnouncementNotificationById(id);

                if (foundNotification == null)
                {
                    return StatusCode(404, "Notification not found");
                }

                await _employeeNotificationService.MarkEmployeeAnnouncementNotificationAsRead(id);
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
