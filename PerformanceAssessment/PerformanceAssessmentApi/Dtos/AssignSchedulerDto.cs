namespace PerformanceAssessmentApi.Dtos
{
    public class AssignSchedulerDto
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsAnswered { get; set; }
        public string? DueDate { get; set; }
        public string? Time { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
