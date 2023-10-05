using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeUpdationDto
    {
        [Required(ErrorMessage = "The userId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The teamId is required.")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "The status is required.")]
        public string? Status { get; set; }
    }
}
