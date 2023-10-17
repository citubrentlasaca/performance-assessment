using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;
        private readonly IMapper _mapper;

        public AnnouncementService(IAnnouncementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Announcement> CreateAnnouncement(AnnouncementCreationDto announcement)
        {
            var model = _mapper.Map<Announcement>(announcement);
            model.Id = await _repository.CreateAnnouncement(model);

            return model;
        }

        public Task<IEnumerable<AnnouncementDto>> GetAllAnnouncements()
        {
            return _repository.GetAllAnnouncements();
        }

        public Task<AnnouncementDto> GetAnnouncementById(int id)
        {
            return _repository.GetAnnouncementById(id);
        }

        public async Task<int> UpdateAnnouncement(int id, AnnouncementUpdationDto announcement)
        {
            var model = _mapper.Map<Announcement>(announcement);
            model.Id = id;

            return await _repository.UpdateAnnouncement(model);
        }

        public async Task<int> DeleteAnnouncement(int id)
        {
            return await _repository.DeleteAnnouncement(id);
        }
    }
}