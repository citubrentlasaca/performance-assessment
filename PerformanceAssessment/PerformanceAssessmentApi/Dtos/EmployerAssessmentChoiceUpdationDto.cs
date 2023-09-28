using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class EmployerAssessmentChoiceUpdationDto
    {
        [Required(ErrorMessage = "The choiceValue is required.")]
        public string? ChoiceValue { get; set; }
    }
}
