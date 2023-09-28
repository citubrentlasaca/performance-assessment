﻿using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class EmployerAssessmentItemCreationDto
    {
        [Required(ErrorMessage = "The question is required.")]
        public string? Question { get; set; }

        [Required(ErrorMessage = "The questionType is required.")]
        public string? QuestionType { get; set; }

        [Required(ErrorMessage = "The weight is required.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "The target is required.")]
        public float Target { get; set; }

        [Required(ErrorMessage = "The required field is required.")]
        public bool Required { get; set; }

        [Required(ErrorMessage = "The employerAssessmentId is required.")]
        public int EmployerAssessmentId { get; set; }
    }
}
