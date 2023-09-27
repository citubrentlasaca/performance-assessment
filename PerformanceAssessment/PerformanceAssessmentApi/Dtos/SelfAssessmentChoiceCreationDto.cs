using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class SelfAssessmentChoiceCreationDto
    {
        [Required(ErrorMessage = "The choiceValue is required.")]
        public string? ChoiceValue { get; set; }

        [Required(ErrorMessage = "The selfAssessmentItemId is required.")]
        public int SelfAssessmentItemId { get; set; }
    }
}
