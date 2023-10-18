using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class AdminNotificationCreationDto
    {
        [Required(ErrorMessage = "The employeeId is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The employeeName is required.")]
        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "The assessmentTitle is required.")]
        public string? AssessmentTitle { get; set; }

        [Required(ErrorMessage = "The teamName is required.")]
        public string? TeamName { get; set; }
    }
}
