using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class ItemUpdationDto
    {
        [Required(ErrorMessage = "The question is required.")]
        public string? Question { get; set; }

        [Required(ErrorMessage = "The questionType is required.")]
        public string? QuestionType { get; set; }

        [Required(ErrorMessage = "The weight is required.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "The required field is required.")]
        public bool Required { get; set; }
    }
}
