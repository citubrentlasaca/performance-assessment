using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IPeerAssessmentRepository
    {
        Task<int> CreatePeerAssessment(PeerAssessment assessment);

        Task<IEnumerable<PeerAssessmentDto>> GetAllPeerAssessments();

        Task<PeerAssessmentDto> GetPeerAssessmentById(int id);

        Task<int> UpdatePeerAssessment(PeerAssessment assessment);

        Task<int> DeletePeerAssessment(int id);

        Task<PeerAssessmentItemsDto?> GetPeerAssessmentItemsById(int id);
    }
}
