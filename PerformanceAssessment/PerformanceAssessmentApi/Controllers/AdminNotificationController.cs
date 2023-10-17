﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/admin-notifications")]
    [ApiController]
    public class AdminNotificationController : ControllerBase
    {
        private readonly IAdminNotificationService _adminNotificationService;
        private readonly ILogger<AdminNotificationController> _logger;

        public AdminNotificationController(IAdminNotificationService adminNotificationService, ILogger<AdminNotificationController> logger)
        {
            _adminNotificationService = adminNotificationService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new admin notification
        /// </summary>
        /// <param name="adminNotification">Admin notification details</param>
        /// <returns>Returns the newly created admin notification</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/admin-notifications
        ///     {
        ///         "employeeId": 1,
        ///         "assessmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new admin notification</response>
        /// <response code="400">Admin notification details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateAdminNotification")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AdminNotificationCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAdminNotification([FromBody] AdminNotificationCreationDto adminNotification)
        {
            try
            {
                // Create a new admin notification
                var newAdminNotification = await _adminNotificationService.CreateAdminNotification(adminNotification);

                return CreatedAtRoute("GetAdminNotificationById", new { id = newAdminNotification.Id }, newAdminNotification);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all admin notifications
        /// </summary>
        /// <returns>Returns all admin notifications</returns>
        /// <response code="200">Admin notifications found</response>
        /// <response code="204">No admin notifications found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllAdminNotifications")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAdminNotifications()
        {
            try
            {
                var adminNotifications = await _adminNotificationService.GetAllAdminNotifications();

                if (adminNotifications.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(adminNotifications);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the admin notification by id
        /// </summary>
        /// <param name="id">Admin notification id</param>
        /// <returns>Returns the details of an admin notification with id <paramref name="id"/></returns>
        /// <response code="200">Admin notification found</response>
        /// <response code="404">Admin notification not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetAdminNotificationById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminNotificationById(int id)
        {
            try
            {
                // Check if admin notification exists
                var foundAdminNotification = await _adminNotificationService.GetAdminNotificationById(id);

                if (foundAdminNotification == null)
                {
                    return StatusCode(404, "Admin notification not found");
                }

                return Ok(foundAdminNotification);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}