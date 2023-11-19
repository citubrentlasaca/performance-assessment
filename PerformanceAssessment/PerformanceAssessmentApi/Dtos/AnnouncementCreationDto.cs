using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class AnnouncementCreationDto
    {
        [Required(ErrorMessage = "The teamId is required.")]
        public int TeamId { get; set; }
        [Required(ErrorMessage = "The content is required.")]
        public string? Content { get; set; }
    }
}
