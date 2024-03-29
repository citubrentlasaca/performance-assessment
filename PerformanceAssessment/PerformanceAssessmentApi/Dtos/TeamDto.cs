﻿namespace PerformanceAssessmentApi.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string? Organization { get; set; }
        public string? BusinessType { get; set; }
        public string? BusinessAddress { get; set; }
        public Guid TeamCode { get; set; }
        public string? DateTimeCreated { get; set; }
        public string? DateTimeUpdated { get; set; }
    }
}
