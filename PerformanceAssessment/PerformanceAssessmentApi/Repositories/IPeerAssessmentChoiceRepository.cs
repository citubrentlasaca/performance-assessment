using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IPeerAssessmentChoiceRepository
    {
        Task<int> CreatePeerAssessmentChoice(PeerAssessmentChoice choice);

        Task<IEnumerable<PeerAssessmentChoiceDto>> GetAllPeerAssessmentChoices();

        Task<PeerAssessmentChoiceDto> GetPeerAssessmentChoiceById(int id);

        Task<int> UpdatePeerAssessmentChoice(PeerAssessmentChoice choice);

        Task<int> DeletePeerAssessmentChoice(int id);
    }
}
