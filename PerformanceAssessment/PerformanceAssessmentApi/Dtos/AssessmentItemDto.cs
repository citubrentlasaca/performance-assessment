namespace PerformanceAssessmentApi.Dtos
{
    public class AssessmentItemDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<ItemDto> Items { get; set; } = new List<ItemDto>();
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
