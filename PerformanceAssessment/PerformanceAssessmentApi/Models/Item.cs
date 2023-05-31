namespace PerformanceAssessmentApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public bool Required { get; set; }
        public int AssessmentId { get; set; }
    }
}
