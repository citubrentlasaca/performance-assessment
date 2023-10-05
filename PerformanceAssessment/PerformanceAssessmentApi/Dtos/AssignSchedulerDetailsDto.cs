namespace PerformanceAssessmentApi.Dtos
{
    public class AssignSchedulerDetailsDto
    {
        public List<int> EmployeeIds { get; set; }
        public AssignSchedulerCreationDto Scheduler { get; set; }
    }
}
