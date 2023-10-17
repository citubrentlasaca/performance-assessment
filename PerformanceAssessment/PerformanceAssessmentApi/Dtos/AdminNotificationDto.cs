namespace PerformanceAssessmentApi.Dtos
{
    public class AdminNotificationDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AssessmentId { get; set; }
        public string? DateTimeCreated { get; set; }
    }
}
