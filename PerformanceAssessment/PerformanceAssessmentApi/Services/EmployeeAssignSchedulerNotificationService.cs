using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class EmployeeAssignSchedulerNotificationService : IEmployeeAssignSchedulerNotificationService
    {
        private readonly IEmployeeAssignSchedulerNotificationRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeAssignSchedulerNotificationService(IEmployeeAssignSchedulerNotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeAssignSchedulerNotification> CreateEmployeeAssignSchedulerNotification(EmployeeAssignSchedulerNotificationCreationDto employeeNotification)
        {
            var model = _mapper.Map<EmployeeAssignSchedulerNotification>(employeeNotification);
            model.Id = await _repository.CreateEmployeeAssignSchedulerNotification(model);

            return model;
        }

        public Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotifications()
        {
            return _repository.GetAllEmployeeAssignSchedulerNotifications();
        }

        public Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(int employeeId)
        {
            return _repository.GetAllEmployeeAssignSchedulerNotificationsByEmployeeId(employeeId);
        }

        public Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeAssignSchedulerNotificationById(int id)
        {
            return _repository.GetEmployeeAssignSchedulerNotificationById(id);
        }
    }
}