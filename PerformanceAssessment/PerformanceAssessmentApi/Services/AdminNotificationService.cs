using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class AdminNotificationService : IAdminNotificationService
    {
        private readonly IAdminNotificationRepository _repository;
        private readonly IMapper _mapper;

        public AdminNotificationService(IAdminNotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AdminNotification> CreateAdminNotification(AdminNotificationCreationDto adminNotification)
        {
            var model = _mapper.Map<AdminNotification>(adminNotification);
            model.Id = await _repository.CreateAdminNotification(model);

            return model;
        }

        public Task<IEnumerable<AdminNotificationDto>> GetAllAdminNotifications()
        {
            return _repository.GetAllAdminNotifications();
        }

        public Task<AdminNotificationDto> GetAdminNotificationById(int id)
        {
            return _repository.GetAdminNotificationById(id);
        }
    }
}