﻿using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class AnswerCreationDto
    {
        [Required(ErrorMessage = "The itemId is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "The answerText is required.")]
        public string? AnswerText { get; set; }

        [Required(ErrorMessage = "The selectedChoices is required.")]
        public string? SelectedChoices { get; set; }

        [Required(ErrorMessage = "The counterValue is required.")]
        public float? CounterValue { get; set; }
    }
}
