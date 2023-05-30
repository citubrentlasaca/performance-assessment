namespace PerformanceAssessmentApi.Dtos
{
    public class AssessmentItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<ItemDto> Items { get; set; } = new List<ItemDto>();
    }
}
