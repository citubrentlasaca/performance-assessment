using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class ChoiceCreationDto
    {
        [Required(ErrorMessage = "The choiceValue is required.")]
        public string? ChoiceValue { get; set; }

        [Required(ErrorMessage = "The weight is required.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "The itemId is required.")]
        public int ItemId { get; set; }
    }
}
