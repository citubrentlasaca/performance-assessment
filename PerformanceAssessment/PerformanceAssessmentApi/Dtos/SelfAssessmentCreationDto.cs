using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class SelfAssessmentCreationDto
    {
        [Required(ErrorMessage = "The title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        public string? Description { get; set; }
    }
}
