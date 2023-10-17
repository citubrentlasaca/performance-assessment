namespace PerformanceAssessmentApi.Models
{
    public class EmployeeNotification
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AssessmentId { get; set; }
        public int AnnouncementId { get; set; }
        public string? DateTimeCreated { get; set; }
    }
}
