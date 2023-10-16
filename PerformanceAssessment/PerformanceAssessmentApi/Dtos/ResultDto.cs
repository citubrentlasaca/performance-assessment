namespace PerformanceAssessmentApi.Dtos
{
    public class ResultDto
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int EmployeeId { get; set; }
        public float Score { get; set; }
        public string? DateTimeDue { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
