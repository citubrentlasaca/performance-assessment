namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeNotificationDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AssessmentId { get; set; }
        public int AnnouncementId { get; set; }
        public string? DateTimeCreated { get; set; }
    }
}
