namespace PerformanceAssessmentApi.Models
{
    public class EmployerAssessmentChoice
    {
        public int Id { get; set; }
        public string? ChoiceValue { get; set; }
        public int EmployerAssessmentItemId { get; set; }
    }
}
