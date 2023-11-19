using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeCreationDto
    {
        [Required(ErrorMessage = "The userId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The teamId is required.")]
        public int TeamId { get; set; }
    }
}
