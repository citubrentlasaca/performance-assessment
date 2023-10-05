using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class TeamCreationDto
    {
        [Required(ErrorMessage = "The organization is required.")]
        public string? Organization { get; set; }

        [Required(ErrorMessage = "The firstName is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The lastName is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The businessType is required.")]
        public string? BusinessType { get; set; }

        [Required(ErrorMessage = "The businessAddress is required.")]
        public string? BusinessAddress { get; set; }
    }
}
