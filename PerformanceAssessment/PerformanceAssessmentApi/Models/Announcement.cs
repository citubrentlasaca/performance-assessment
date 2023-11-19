namespace PerformanceAssessmentApi.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string? Content { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
