namespace PerformanceAssessmentApi.Models
{
    public class SelfAssessmentChoice
    {
        public int Id { get; set; }
        public string? ChoiceValue { get; set; }
        public int SelfAssessmentItemId { get; set; }
    }
}
