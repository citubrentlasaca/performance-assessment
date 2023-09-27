namespace PerformanceAssessmentApi.Models
{
    public class PeerAssessmentItem
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public float Target { get; set; }
        public bool Required { get; set; }
        public int PeerAssessmentId { get; set; }
    }
}
