﻿namespace PerformanceAssessmentApi.Dtos
{
    public class AssessmentDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
