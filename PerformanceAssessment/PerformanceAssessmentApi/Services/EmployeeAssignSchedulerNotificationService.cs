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

        public async Task<EmployeeAssignSchedulerNotification> CreateEmployeeNotification(EmployeeAssignSchedulerNotificationCreationDto employeeNotification)
        {
            var model = _mapper.Map<EmployeeAssignSchedulerNotification>(employeeNotification);
            model.Id = await _repository.CreateEmployeeNotification(model);

            return model;
        }

        public Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotifications()
        {
            return _repository.GetAllEmployeeNotifications();
        }

        public Task<IEnumerable<EmployeeAssignSchedulerNotificationDto>> GetAllEmployeeNotificationsByEmployeeId(int employeeId)
        {
            return _repository.GetAllEmployeeNotificationsByEmployeeId(employeeId);
        }

        public Task<EmployeeAssignSchedulerNotificationDto> GetEmployeeNotificationById(int id)
        {
            return _repository.GetEmployeeNotificationById(id);
        }
    }
}