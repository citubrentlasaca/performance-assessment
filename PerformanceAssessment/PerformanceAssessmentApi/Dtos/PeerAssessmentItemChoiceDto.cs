namespace PerformanceAssessmentApi.Dtos
{
    public class PeerAssessmentItemChoiceDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public float Target { get; set; }
        public bool Required { get; set; }
        public int PeerAssessmentId { get; set; }
        public List<PeerAssessmentChoiceDto> PeerAssessmentChoices { get; set; } = new List<PeerAssessmentChoiceDto>();
    }
}
