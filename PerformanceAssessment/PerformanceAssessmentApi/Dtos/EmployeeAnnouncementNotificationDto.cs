namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeAnnouncementNotificationDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AnnouncementId { get; set; }
        public string? DateTimeCreated { get; set; }
    }
}
