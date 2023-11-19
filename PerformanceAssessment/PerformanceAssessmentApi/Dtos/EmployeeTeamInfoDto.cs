using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeTeamInfoDto
    {
        [Required(ErrorMessage = "The userId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The teamCode is required.")]
        public Guid TeamCode { get; set; }

        public string Role = "User";

        public string Status = "Active";
    }
}
