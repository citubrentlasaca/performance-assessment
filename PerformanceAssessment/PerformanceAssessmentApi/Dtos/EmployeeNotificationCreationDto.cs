using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeNotificationCreationDto
    {
        [Required(ErrorMessage = "The employeeId is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The assessmentId is required.")]
        public int AssessmentId { get; set; }

        [Required(ErrorMessage = "The announcementId is required.")]
        public int AnnouncementId { get; set; }
    }
}
