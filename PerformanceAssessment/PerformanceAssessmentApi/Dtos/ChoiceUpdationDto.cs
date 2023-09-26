using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class ChoiceUpdationDto
    {
        [Required(ErrorMessage = "The choiceValue is required.")]
        public string? ChoiceValue { get; set; }
    }
}
