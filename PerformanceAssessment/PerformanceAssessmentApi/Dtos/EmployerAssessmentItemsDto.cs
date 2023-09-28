namespace PerformanceAssessmentApi.Dtos
{
    public class EmployerAssessmentItemsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<EmployerAssessmentItemDto> EmployerAssessmentItems { get; set; } = new List<EmployerAssessmentItemDto>();
    }
}
