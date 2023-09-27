﻿namespace PerformanceAssessmentApi.Dtos
{
    public class SelfAssessmentItemChoiceDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public float Weight { get; set; }
        public float Target { get; set; }
        public bool Required { get; set; }
        public int SelfAssessmentId { get; set; }
        public List<SelfAssessmentChoiceDto> SelfAssessmentChoices { get; set; } = new List<SelfAssessmentChoiceDto>();
    }
}