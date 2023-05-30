namespace PerformanceAssessmentApi.Dtos
{
    public class ItemChoiceDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public bool Required { get; set; }
        public int AssessmentId { get; set; }
        public List<ChoiceDto> Choices { get; set; } = new List<ChoiceDto>();
    }
}
