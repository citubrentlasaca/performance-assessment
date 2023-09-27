namespace PerformanceAssessmentApi.Dtos
{
    public class SelfAssessmentItemsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<SelfAssessmentItemDto> SelfAssessmentItems { get; set; } = new List<SelfAssessmentItemDto>();
    }
}
