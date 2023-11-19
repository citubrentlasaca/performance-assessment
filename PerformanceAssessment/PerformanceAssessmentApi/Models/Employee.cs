namespace PerformanceAssessmentApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public string? DateTimeJoined { get; set; }
    }
}
