namespace PerformanceAssessmentApi.Dtos
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
        public string? Status { get; set; }
        public string? DateTimeJoined { get; set; }
    }
}
