using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/announcements")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private readonly ILogger<AnnouncementController> _logger;

        public AnnouncementController(IAnnouncementService announcementService, ILogger<AnnouncementController> logger)
        {
            _announcementService = announcementService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new announcement
        /// </summary>
        /// <param name="announcement">Announcement details</param>
        /// <returns>Returns the newly created announcement</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/announcements
        ///     {
        ///         "content": "Dear users, we will be performing scheduled maintenance on our system on October 17, 2023. During this time, the system will be temporarily unavailable. We apologize for any inconvenience this may cause and appreciate your understanding. Thank you for being a valued part of our community!"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new announcement</response>
        /// <response code="400">Announcement details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateAnnouncement")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AnnouncementCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementCreationDto announcement)
        {
            try
            {
                // Create a new announcement
                var newAnnouncement = await _announcementService.CreateAnnouncement(announcement);

                return CreatedAtRoute("GetAnnouncementById", new { id = newAnnouncement.Id }, newAnnouncement);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all announcements
        /// </summary>
        /// <returns>Returns all announcements</returns>
        /// <response code="200">Announcements found</response>
        /// <response code="204">No Announcements found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllAnnouncements")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetAllAnnouncements();

                if (announcements.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(announcements);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the announcement by id
        /// </summary>
        /// <param name="id">Announcement id</param>
        /// <returns>Returns the details of an announcement with id <paramref name="id"/></returns>
        /// <response code="200">Announcement found</response>
        /// <response code="404">Announcement not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetAnnouncementById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            try
            {
                // Check if announcement exists
                var foundAnnouncement = await _announcementService.GetAnnouncementById(id);

                if (foundAnnouncement == null)
                {
                    return StatusCode(404, "Announcement not found");
                }

                return Ok(foundAnnouncement);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing announcement
        /// </summary>
        /// <param name="id">The id of the announcement that will be updated</param>
        /// <param name="announcement">New announcement details</param>
        /// <returns>Returns the details of announcement with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/announcements
        ///     {
        ///         "content": "Dear team members, it's that time of the year again! We are approaching the end of the quarter, and it's time for our quarterly performance assessments. Your feedback and self-assessment are vital in helping us measure progress and set new goals. Please be prepared to discuss your achievements, challenges, and objectives with your respective managers. We look forward to a productive assessment process."
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the announcement</response>
        /// <response code="404">Announcement not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateAnnouncement")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AnnouncementUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] AnnouncementUpdationDto announcement)
        {
            try
            {
                // Check if assessment exists
                var foundAnnouncement = await _announcementService.GetAnnouncementById(id);

                if (foundAnnouncement == null)
                {
                    return StatusCode(404, "Announcement not found");
                }

                // Update the assessment
                await _announcementService.UpdateAnnouncement(id, announcement);
                return Ok("Announcement updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing announcement
        /// </summary>
        /// <param name="id">The id of the announcement that will be deleted</param>
        /// <response code="200">Successfully deleted the announcement</response>
        /// <response code="404">Announcement not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteAnnouncement")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                // Check if announcement exists
                var foundAnnouncement = await _announcementService.GetAnnouncementById(id);

                if (foundAnnouncement == null)
                {
                    return StatusCode(404, "Announcement not found");
                }

                await _announcementService.DeleteAnnouncement(id);
                return Ok("Announcement deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}