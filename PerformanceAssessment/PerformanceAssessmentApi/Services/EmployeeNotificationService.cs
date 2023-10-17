using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class EmployeeNotificationService : IEmployeeNotificationService
    {
        private readonly IEmployeeNotificationRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeNotificationService(IEmployeeNotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeNotification> CreateEmployeeNotification(EmployeeNotificationCreationDto employeeNotification)
        {
            var model = _mapper.Map<EmployeeNotification>(employeeNotification);
            model.Id = await _repository.CreateEmployeeNotification(model);

            return model;
        }

        public Task<IEnumerable<EmployeeNotificationDto>> GetAllEmployeeNotifications()
        {
            return _repository.GetAllEmployeeNotifications();
        }

        public Task<EmployeeNotificationDto> GetEmployeeNotificationById(int id)
        {
            return _repository.GetEmployeeNotificationById(id);
        }
    }
}