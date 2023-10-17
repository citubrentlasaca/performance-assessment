using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Services
{
    public interface IAnnouncementService
    {
        Task<Announcement> CreateAnnouncement(AnnouncementCreationDto announcement);

        Task<IEnumerable<AnnouncementDto>> GetAllAnnouncements();

        Task<AnnouncementDto> GetAnnouncementById(int id);

        Task<int> UpdateAnnouncement(int id, AnnouncementUpdationDto announcement);

        Task<int> DeleteAnnouncement(int id);
    }
}
