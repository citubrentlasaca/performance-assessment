using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IPeerAssessmentService
    {
        Task<PeerAssessment> CreatePeerAssessment(PeerAssessmentCreationDto assessment);

        Task<IEnumerable<PeerAssessmentDto>> GetAllPeerAssessments();

        Task<PeerAssessmentDto> GetPeerAssessmentById(int id);

        Task<int> UpdatePeerAssessment(int id, PeerAssessmentUpdationDto assessment);

        Task<int> DeletePeerAssessment(int id);

        Task<PeerAssessmentItemsDto?> GetPeerAssessmentItemsById(int id);
    }
}
