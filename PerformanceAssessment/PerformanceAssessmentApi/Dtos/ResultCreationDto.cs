using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class ResultCreationDto
    {
        [Required(ErrorMessage = "The assessmentId is required.")]
        public int AssessmentId { get; set; }

        [Required(ErrorMessage = "The employeeId is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The score is required.")]
        public float Score { get; set; }

        [Required(ErrorMessage = "The dueDate is required.")]
        public string? DateTimeDue { get; set; }
    }
}
