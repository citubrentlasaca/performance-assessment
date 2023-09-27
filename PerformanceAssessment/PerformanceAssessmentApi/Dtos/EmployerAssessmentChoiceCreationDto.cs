using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class EmployerAssessmentChoiceCreationDto
    {
        [Required(ErrorMessage = "The choiceValue is required.")]
        public string? ChoiceValue { get; set; }

        [Required(ErrorMessage = "The employerAssessmentItemId is required.")]
        public int EmployerAssessmentItemId { get; set; }
    }
}
