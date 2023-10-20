using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class AssignSchedulerCreationDto
    {
        [Required(ErrorMessage = "The assessmentId is required.")]
        public int AssessmentId { get; set; }

        public string? Occurrence { get; set; }

        [Required(ErrorMessage = "The dueDate is required.")]
        public string? DueDate { get; set; }

        [Required(ErrorMessage = "The time is required.")]
        public string? Time { get; set; }
    }
}
