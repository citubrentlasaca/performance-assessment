namespace PerformanceAssessmentApi.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public int EmployeeId {  get; set; }
        public int ItemId { get; set; }
        public string? AnswerText { get; set; }
        public string? SelectedChoices { get; set; }
        public float? CounterValue { get; set; }
        public bool IsDeleted { get; set; }
        public string? DateTimeAnswered { get; set; }
    }
}
