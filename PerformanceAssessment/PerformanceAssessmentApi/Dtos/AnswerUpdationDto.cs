using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class AnswerUpdationDto
    {
        [Required(ErrorMessage = "The resultId is required.")]
        public int ResultId { get; set; }

        [Required(ErrorMessage = "The employeeId is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The itemId is required.")]
        public int ItemId { get; set; }

        public string? AnswerText { get; set; }

        public string? SelectedChoices { get; set; }

        public float? CounterValue { get; set; }
    }
}
