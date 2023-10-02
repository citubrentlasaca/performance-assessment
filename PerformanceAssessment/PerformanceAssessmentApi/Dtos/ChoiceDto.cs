namespace PerformanceAssessmentApi.Dtos
{
    public class ChoiceDto
    {
        public int Id { get; set; }
        public string? ChoiceValue { get; set; }
        public float Weight { get; set; }
        public int ItemId { get; set; }
    }
}
