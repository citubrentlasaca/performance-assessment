using System.ComponentModel.DataAnnotations;

namespace PerformanceAssessmentApi.Dtos
{
    public class PeerAssessmentChoiceCreationDto
    {
        [Required(ErrorMessage = "The choiceValue is required.")]
        public string? ChoiceValue { get; set; }

        [Required(ErrorMessage = "The peerAssessmentItemId is required.")]
        public int PeerAssessmentItemId { get; set; }
    }
}
