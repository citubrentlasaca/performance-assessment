namespace PerformanceAssessmentApi.Dtos
{
    public class AssessmentAnswersDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
        public List<ItemAnswersDto> Items { get; set; } = new List<ItemAnswersDto>();
    }
}
