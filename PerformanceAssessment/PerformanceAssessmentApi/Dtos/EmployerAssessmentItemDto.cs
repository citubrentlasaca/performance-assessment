namespace PerformanceAssessmentApi.Dtos
{
    public class EmployerAssessmentItemDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public float Target { get; set; }
        public bool Required { get; set; }
        public List<EmployerAssessmentChoiceDto> EmployerAssessmentChoices { get; set; } = new List<EmployerAssessmentChoiceDto>();
        public int EmployerAssessmentId { get; set; }
    }
}
