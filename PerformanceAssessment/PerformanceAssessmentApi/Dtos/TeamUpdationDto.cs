using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class TeamUpdationDto
    {
        [Required(ErrorMessage = "The organization is required.")]
        public string? Organization { get; set; }

        [Required(ErrorMessage = "The businessType is required.")]
        public string? BusinessType { get; set; }

        [Required(ErrorMessage = "The businessAddress is required.")]
        public string? BusinessAddress { get; set; }
    }
}
