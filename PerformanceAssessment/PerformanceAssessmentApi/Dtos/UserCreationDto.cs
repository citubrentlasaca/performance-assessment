using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class UserCreationDto
    {
        [Required(ErrorMessage = "The firstName is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The lastName is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The role is required.")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "The emailAddress is required.")]
        [RegularExpression(
            @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$",
            ErrorMessage = "The Email Address is not valid"
        )]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        public string? Password { get; set; }
    }
}
