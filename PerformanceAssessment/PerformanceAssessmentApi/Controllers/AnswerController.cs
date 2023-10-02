﻿using Microsoft.AspNetCore.Mvc;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/answers")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly ILogger<AnswerController> _logger;

        public AnswerController(IAnswerService answerService, ILogger<AnswerController> logger)
        {
            _answerService = answerService;
            _logger = logger;
        }

        /// <summary>
        /// Saves answers
        /// </summary>
        /// <param name="answers">Answer details</param>
        /// <returns>Returns the newly created answers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/answers
        ///     {
        ///         "asssessmentId": 1,
        ///         "itemId": 1,
        ///         "answerText": "My performance was great.",
        ///         "selectedChoices": "Efficiency, Teamwork",
        ///         "counterValue": 10
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created new answers</response>
        /// <response code="400">Answer details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "SaveAnswers")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AnswerCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveAnswers([FromBody] AnswerCreationDto answers)
        {
            try
            {
                // Create new answers
                var newAnswers = await _answerService.SaveAnswers(answers);

                return CreatedAtRoute("GetAnswersById", new { id = newAnswers.Id }, newAnswers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the answers by assessment id
        /// </summary>
        /// <param name="assessmentId">Assessment id</param>
        /// <returns>Returns the details of the answers with assessment id <paramref name="assessmentId"/></returns>
        /// <response code="200">Answers found</response>
        /// <response code="404">Answers not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("assessments/{assessmentId}", Name = "GetAnswersByAssessmentId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnswersByAssessmentId(int assessmentId)
        {
            try
            {
                // Check if the answers exist
                var foundAnswers = await _answerService.GetAnswersByAssessmentId(assessmentId);

                if (foundAnswers == null)
                {
                    return StatusCode(404, "Answers not found");
                }

                return Ok(foundAnswers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the answers by item id
        /// </summary>
        /// <param name="itemId">Item id</param>
        /// <returns>Returns the details of the answers with item id <paramref name="itemId"/></returns>
        /// <response code="200">Answers found</response>
        /// <response code="404">Answers not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("items/{itemId}", Name = "GetAnswersByItemId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnswersByItemId(int itemId)
        {
            try
            {
                // Check if the answers exist
                var foundAnswers = await _answerService.GetAnswersByItemId(itemId);

                if (foundAnswers == null)
                {
                    return StatusCode(404, "Answers not found");
                }

                return Ok(foundAnswers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the answers by id
        /// </summary>
        /// <param name="id">Answer id</param>
        /// <returns>Returns the details of the answers with answer id <paramref name="id"/></returns>
        /// <response code="200">Answers found</response>
        /// <response code="404">Answers not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetAnswersById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnswersById(int id)
        {
            try
            {
                // Check if the answers exist
                var foundAnswers = await _answerService.GetAnswersById(id);

                if (foundAnswers == null)
                {
                    return StatusCode(404, "Answers not found");
                }

                return Ok(foundAnswers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates the existing answers
        /// </summary>
        /// <param name="id">The id of the answer that will be updated</param>
        /// <param name="answers">New answer details</param>
        /// <returns>Returns the details of assessment with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/answers
        ///     {
        ///         "assessmentId": 2,
        ///         "itemId": 2,
        ///         "answerText": "The team was good.",
        ///         "selectedChoices": "Punctuality, Evaluation",
        ///         "counterValue": 5
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated the answers</response>
        /// <response code="404">Answers not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateAnswers")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AnswerUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAnswers(int id, [FromBody] AnswerUpdationDto answers)
        {
            try
            {
                // Check if the answers exists
                var foundAnswers = await _answerService.GetAnswersById(id);

                if (foundAnswers == null)
                {
                    return StatusCode(404, "Answers not found");
                }

                // Update the answers
                await _answerService.UpdateAnswers(id, answers);
                return Ok("Answers updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes the existing answers
        /// </summary>
        /// <param name="id">The id of the answer that will be deleted</param>
        /// <response code="200">Successfully deleted the answers</response>
        /// <response code="404">Answers not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteAnswers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAnswers(int id)
        {
            try
            {
                // Check if the answers exist
                var foundAnswers = await _answerService.GetAnswersById(id);

                if (foundAnswers == null)
                {
                    return StatusCode(404, "Answers not found");
                }

                await _answerService.DeleteAnswers(id);
                return Ok("Answers deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}