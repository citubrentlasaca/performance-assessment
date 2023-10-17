using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class AnnouncementUpdationDto
    {
        [Required(ErrorMessage = "The content is required.")]
        public string? Content { get; set; }
    }
}
