namespace PerformanceAssessmentApi.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public float Target { get; set; }
        public bool Required { get; set; }
        public List<ChoiceDto> Choices { get; set; } = new List<ChoiceDto>();
        public int AssessmentId { get; set; }
    }
}
