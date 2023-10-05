namespace PerformanceAssessmentApi.Models
{
    public class AssignScheduler
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int EmployeeId { get; set; }
        public string? Reminder { get; set; }
        public string? Occurrence { get; set; }
        public string? DueDate { get; set; }
        public string? Time { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
