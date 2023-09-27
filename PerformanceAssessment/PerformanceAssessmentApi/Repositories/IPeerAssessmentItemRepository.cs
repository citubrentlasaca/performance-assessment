using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IPeerAssessmentItemRepository
    {
        Task<int> CreatePeerAssessmentItem(PeerAssessmentItem item);

        Task<IEnumerable<PeerAssessmentItemChoiceDto>> GetAllPeerAssessmentItems();

        Task<PeerAssessmentItemChoiceDto> GetPeerAssessmentItemById(int id);

        Task<int> UpdatePeerAssessmentItem(PeerAssessmentItem item);

        Task<int> DeletePeerAssessmentItem(int id);
    }
}
