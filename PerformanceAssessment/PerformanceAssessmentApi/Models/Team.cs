namespace PerformanceAssessmentApi.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string? Organization { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BusinessType { get; set; }
        public string? BusinessAddress { get; set; }
        public Guid TeamCode { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
