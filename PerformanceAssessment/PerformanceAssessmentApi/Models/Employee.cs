namespace PerformanceAssessmentApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? TeamId { get; set; }
        public string? Status { get; set; }
        public string? DateTimeJoined { get; set; }
    }
}
