using Microsoft.AspNetCore.Mvc;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly ILogger<AnalyticsController> _logger;

        public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
        {
            _analyticsService = analyticsService;
            _logger = logger;
        }

        /// <summary>
        /// Gets employees' performance based on assessment ID
        /// </summary>
        /// <param name="assessmentId">Assessment ID</param>
        /// <returns>Returns the performance data</returns>
        /// <response code="200">Performance data found</response>
        /// <response code="404">Performance data not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("performance/get-analytics-by-assessmentId", Name = "GetEmployeesPerformanceByAssessmentId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeesPerformanceByAssessmentId([FromQuery(Name = "assessmentId")] int assessmentId)
        {
            try
            {
                var performanceData = await _analyticsService.GetEmployeesPerformanceByAssessmentId(assessmentId);

                if (performanceData == null)
                {
                    return StatusCode(404, "Performance data not found");
                }

                return Ok(performanceData);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets employees' performance based on assessment ID and month number
        /// </summary>
        /// <param name="assessmentId">Assessment ID</param>
        /// <param name="monthNumber">Month Number (e.g., "1 for January, 2 for February, etc.")</param>
        /// <returns>Returns the performance data</returns>
        /// <response code="200">Performance data found</response>
        /// <response code="404">Performance data not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("performance/get-analytics-by-assessmentId-and-monthNumber", Name = "GetEmployeesPerformanceByAssessmentIdAndMonthNumber")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeesPerformanceByAssessmentIdAndMonthNumber([FromQuery(Name = "assessmentId")] int assessmentId, [FromQuery(Name = "monthNumber")] int monthNumber)
        {
            try
            {
                var performanceData = await _analyticsService.GetEmployeesPerformanceByAssessmentIdAndMonthNumber(assessmentId, monthNumber);

                if (performanceData == null)
                {
                    return StatusCode(404, "Performance data not found");
                }

                return Ok(performanceData);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets employees' performance based on assessment ID and year
        /// </summary>
        /// <param name="assessmentId">Assessment ID</param>
        /// <param name="year">Year (e.g., 2023)</param>
        /// <returns>Returns the performance data</returns>
        /// <response code="200">Performance data found</response>
        /// <response code="404">Performance data not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("performance/get-analytics-by-assessmentId-and-year", Name = "GetEmployeesPerformanceByAssessmentIdAndYear")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeesPerformanceByAssessmentIdAndYear([FromQuery(Name = "assessmentId")] int assessmentId, [FromQuery(Name = "year")] int year)
        {
            try
            {
                var performanceData = await _analyticsService.GetEmployeesPerformanceByAssessmentIdAndYear(assessmentId, year);

                if (performanceData == null)
                {
                    return StatusCode(404, "Performance data not found");
                }

                return Ok(performanceData);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}