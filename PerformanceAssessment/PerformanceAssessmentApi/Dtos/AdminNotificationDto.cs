namespace PerformanceAssessmentApi.Dtos
{
    public class AdminNotificationDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? AssessmentTitle { get; set; }
        public string? TeamName { get; set; }
        public string? DateTimeCreated { get; set; }
    }
}
