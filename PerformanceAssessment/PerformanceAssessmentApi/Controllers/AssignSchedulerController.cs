using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;
using Hangfire;
using System.Globalization;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/schedulers")]
    [ApiController]
    public class AssignSchedulerController : ControllerBase
    {
        private readonly IAssignSchedulerService _assignSchedulerService;
        private readonly IAssessmentService _assessmentService;
        private readonly IEmployeeAssignSchedulerNotificationService _employeeAssignSchedulerNotificationService;
        private readonly IAdminNotificationService _adminNotificationService;
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly ILogger<AssignSchedulerController> _logger;

        public AssignSchedulerController(IAssignSchedulerService assignSchedulerService,
                                         IAssessmentService assessmentService,
                                         IEmployeeAssignSchedulerNotificationService employeeAssignSchedulerNotificationService,
                                         IAdminNotificationService adminNotificationService,
                                         IEmployeeService employeeService,
                                         IUserService userService,
                                         ITeamService teamService,
                                         ILogger<AssignSchedulerController> logger)
        {
            _assignSchedulerService = assignSchedulerService;
            _assessmentService = assessmentService;
            _employeeAssignSchedulerNotificationService = employeeAssignSchedulerNotificationService;
            _adminNotificationService = adminNotificationService;
            _employeeService = employeeService;
            _userService = userService;
            _teamService = teamService;
            _logger = logger;
        }

        /// <summary>
        /// Creates new schedulers for multiple employees
        /// </summary>
        /// <param name="assignScheduler">Common scheduler details for all employees</param>
        /// <returns>Returns the IDs of the newly created scheduler records</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/schedulers
        ///     {
        ///         "employeeIds": [1, 2, 3, 4, 5],
        ///         "scheduler": {
        ///             "assessmentId": 1,
        ///             "occurrence": "Daily",
        ///             "dueDate": "2023-10-16",
        ///             "time": "14:30"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created new scheduler records</response>
        /// <response code="400">Scheduler details are invalid or employee IDs are empty</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateAssignSchedulers")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AssignSchedulerDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAssignSchedulers([FromBody] AssignSchedulerDetailsDto assignScheduler)
        {
            try
            {
                if (assignScheduler.EmployeeIds == null || assignScheduler.EmployeeIds.Count == 0)
                {
                    return BadRequest("Employee IDs cannot be empty.");
                }

                var insertedIds = await _assignSchedulerService.CreateAssignSchedulers(assignScheduler.EmployeeIds, assignScheduler.Scheduler);
                var scheduler = await _assignSchedulerService.GetAssignSchedulerById(insertedIds.First());
                var assessment = await _assessmentService.GetAssessmentById(scheduler.AssessmentId);
                var employee = await _employeeService.GetEmployeeById(scheduler.EmployeeId);
                var user = await _userService.GetUserById(employee.UserId);
                var team = await _teamService.GetTeamById(employee.TeamId);
                var employeeNotification = new EmployeeAssignSchedulerNotificationCreationDto
                {
                    EmployeeId = scheduler.EmployeeId,
                    AssessmentId = assessment.Id,
                };
                await _employeeAssignSchedulerNotificationService.CreateEmployeeAssignSchedulerNotification(employeeNotification);
                var adminNotification = new AdminNotificationCreationDto
                {
                    EmployeeId = assessment.EmployeeId,
                    EmployeeName = user.FirstName + " " + user.LastName,
                    AssessmentTitle = assessment.Title,
                    TeamName = team.Organization
                };

                var dueDateTime = DateTime.ParseExact($"{scheduler.DueDate} {scheduler.Time}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                var delay = dueDateTime - DateTime.Now;
                var notificationDelay = TimeSpan.FromSeconds(5);
                var notificationScheduledTime = dueDateTime - notificationDelay;
                BackgroundJob.Schedule(() => _adminNotificationService.ExecuteAdminNotificationAsync(insertedIds.First(), adminNotification), notificationScheduledTime);
                BackgroundJob.Schedule(() => DeleteAssignScheduler(scheduler.Id), delay);

                if (assessment.Title == "Daily Performance Report")
                {
                    RecurringJob.AddOrUpdate($"SetIsAnsweredToFalse_{scheduler.Id}", () => _assignSchedulerService.SetIsAnsweredToFalse(scheduler.Id), Cron.Daily, new RecurringJobOptions
                    {
                        TimeZone = TimeZoneInfo.Local
                    });
                }       
                
                return CreatedAtRoute("GetAssignSchedulerById", new { id = insertedIds }, insertedIds);
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
        ///         "isAnswered": true,
        ///         "occurrence": "Once",
        ///         "dueDate": "2023-10-17",
        ///         "time": "15:00"
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

                RecurringJob.RemoveIfExists($"SetIsAnsweredToFalse_{id}");
                await _assignSchedulerService.DeleteAssignScheduler(id);
                return Ok("Scheduler deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the scheduler by employee ID and assessment ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="assessmentId">Assessment ID</param>
        /// <returns>Returns the details of the scheduler with the specified employee ID and assessment ID</returns>
        /// <response code="200">Scheduler found</response>
        /// <response code="404">Scheduler not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("get-by-employee-and-assessment", Name = "GetAssignSchedulerByEmployeeIdAndAssessmentId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignSchedulerByEmployeeIdAndAssessmentId([FromQuery(Name = "employeeId")] int employeeId, [FromQuery(Name = "assessmentId")] int assessmentId)
        {
            try
            {
                // Check if the scheduler exists
                var foundScheduler = await _assignSchedulerService.GetAssignSchedulerByEmployeeIdAndAssessmentId(employeeId, assessmentId);

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
    }
}