using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IPeerAssessmentChoiceService
    {
        Task<PeerAssessmentChoice> CreatePeerAssessmentChoice(PeerAssessmentChoiceCreationDto choice);

        Task<IEnumerable<PeerAssessmentChoiceDto>> GetAllPeerAssessmentChoices();

        Task<PeerAssessmentChoiceDto> GetPeerAssessmentChoiceById(int id);

        Task<int> UpdatePeerAssessmentChoice(int id, PeerAssessmentChoiceUpdationDto choice);

        Task<int> DeletePeerAssessmentChoice(int id);
    }
}
