namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string? Status { get; set; }
        public string? DateTimeJoined { get; set; }
    }
}
