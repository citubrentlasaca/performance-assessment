using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IPeerAssessmentItemService
    {
        Task<PeerAssessmentItem> CreatePeerAssessmentItem(PeerAssessmentItemCreationDto item);

        Task<IEnumerable<PeerAssessmentItemChoiceDto>> GetAllPeerAssessmentItems();

        Task<PeerAssessmentItemChoiceDto> GetPeerAssessmentItemById(int id);

        Task<int> UpdatePeerAssessmentItem(int id, PeerAssessmentItemUpdationDto item);

        Task<int> DeletePeerAssessmentItem(int id);
    }
}
