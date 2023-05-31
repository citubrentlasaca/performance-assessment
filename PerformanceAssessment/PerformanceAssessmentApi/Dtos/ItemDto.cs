namespace PerformanceAssessmentApi.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public bool Required { get; set; }
        public ChoiceDto? Choices { get; set; }
        public int AssessmentId { get; set; }
    }
}
