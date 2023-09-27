using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class SelfAssessmentChoiceUpdationDto
    {
        [Required(ErrorMessage = "The choiceValue is required.")]
        public string? ChoiceValue { get; set; }
    }
}
