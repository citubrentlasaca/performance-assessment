namespace PerformanceAssessmentApi.Dtos
{
    public class SelfAssessmentChoiceDto
    {
        public int Id { get; set; }
        public string? ChoiceValue { get; set; }
        public int SelfAssessmentItemId { get; set; }
    }
}
