using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class AdminNotificationService : IAdminNotificationService
    {
        private readonly IAdminNotificationRepository _repository;
        private readonly IAssignSchedulerRepository _schedulerRepository;
        private readonly IMapper _mapper;

        public AdminNotificationService(IAdminNotificationRepository repository, IAssignSchedulerRepository schedulerRepository, IMapper mapper)
        {
            _repository = repository;
            _schedulerRepository = schedulerRepository;
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

        public Task<IEnumerable<AdminNotificationDto>> GetAdminNotificationByEmployeeId(int employeeId)
        {
            return _repository.GetAdminNotificationByEmployeeId(employeeId);
        }

        public async Task ExecuteAdminNotificationAsync(int schedulerId, AdminNotificationCreationDto adminNotification)
        {
            var currentScheduler = await _schedulerRepository.GetAssignSchedulerById(schedulerId);
            var model = _mapper.Map<AdminNotification>(adminNotification);
            if (!currentScheduler.IsAnswered)
            {
                await _repository.CreateAdminNotification(model);
            }
        }

        public async Task<int> MarkAdminNotificationAsRead(int id)
        {
            return await _repository.MarkAdminNotificationAsRead(id);
        }
    }
}