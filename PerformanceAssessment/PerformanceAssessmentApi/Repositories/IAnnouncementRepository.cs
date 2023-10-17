using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<int> CreateAnnouncement(Announcement announcement);

        Task<IEnumerable<AnnouncementDto>> GetAllAnnouncements();

        Task<AnnouncementDto> GetAnnouncementById(int id);

        Task<int> UpdateAnnouncement(Announcement announcement);

        Task<int> DeleteAnnouncement(int id);
    }
}
