using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class EmployeeAnnouncementNotificationService : IEmployeeAnnouncementNotificationService
    {
        private readonly IEmployeeAnnouncementNotificationRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeAnnouncementNotificationService(IEmployeeAnnouncementNotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<EmployeeAnnouncementNotification> CreateEmployeeAnnouncementNotification(EmployeeAnnouncementNotificationCreationDto employeeNotification)
        {
            var model = _mapper.Map<EmployeeAnnouncementNotification>(employeeNotification);
            model.Id = await _repository.CreateEmployeeAnnouncementNotification(model);

            return model;
        }

        public Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetAllEmployeeAnnouncementNotifications()
        {
            return _repository.GetAllEmployeeAnnouncementNotifications();
        }

        public Task<IEnumerable<EmployeeAnnouncementNotificationDto>> GetEmployeeAnnouncementNotificationByEmployeeId(int id)
        {
            return _repository.GetEmployeeAnnouncementNotificationByEmployeeId(id);
        }

        public Task<EmployeeAnnouncementNotificationDto> GetEmployeeAnnouncementNotificationById(int id)
        {
            return _repository.GetEmployeeAnnouncementNotificationById(id);
        }
    }
}
