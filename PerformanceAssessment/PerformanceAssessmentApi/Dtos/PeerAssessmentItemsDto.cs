namespace PerformanceAssessmentApi.Dtos
{
    public class PeerAssessmentItemsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<PeerAssessmentItemDto> PeerAssessmentItems { get; set; } = new List<PeerAssessmentItemDto>();
    }
}
