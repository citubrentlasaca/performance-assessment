using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class AnnouncementCreationDto
    {
        [Required(ErrorMessage = "The content is required.")]
        public string? Content { get; set; }
    }
}
