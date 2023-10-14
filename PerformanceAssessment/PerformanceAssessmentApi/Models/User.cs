namespace PerformanceAssessmentApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
