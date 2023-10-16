using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Repositories;

namespace PerformanceAssessmentApi.Services
{
    public class AssignSchedulerService : IAssignSchedulerService
    {
        private readonly IAssignSchedulerRepository _repository;
        private readonly IMapper _mapper;

        public AssignSchedulerService(IAssignSchedulerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<int>> CreateAssignSchedulers(IEnumerable<int> employeeIds, AssignSchedulerCreationDto schedule)
        {
            var assignScheduler = _mapper.Map<AssignScheduler>(schedule);
            var insertedIds = await _repository.CreateAssignSchedulers(employeeIds, assignScheduler);

            return insertedIds;
        }

        public Task<IEnumerable<AssignSchedulerDto>> GetAllAssignSchedulers()
        {
            return _repository.GetAllAssignSchedulers();
        }

        public Task<AssignSchedulerDto> GetAssignSchedulerById(int id)
        {
            return _repository.GetAssignSchedulerById(id);
        }

        public Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByAssessmentId(int assessmentId)
        {
            return _repository.GetAssignSchedulerByAssessmentId(assessmentId);
        }

        public Task<IEnumerable<AssignSchedulerDto>> GetAssignSchedulerByEmployeeId(int employeeId)
        {
            return _repository.GetAssignSchedulerByEmployeeId(employeeId);
        }

        public async Task<int> UpdateAssignScheduler(int id, AssignSchedulerUpdationDto schedule)
        {
            var model = _mapper.Map<AssignScheduler>(schedule);
            model.Id = id;

            return await _repository.UpdateAssignScheduler(model);
        }

        public async Task<int> DeleteAssignScheduler(int id)
        {
            return await _repository.DeleteAssignScheduler(id);
        }

        public async Task SetIsAnsweredToFalse(int id)
        {
            // Get the scheduler by ID
            var scheduler = await GetAssignSchedulerById(id);

            if (scheduler != null)
            {
                // Update the scheduler's isAnswered field to false
                scheduler.IsAnswered = false;
                await UpdateAssignScheduler(id, _mapper.Map<AssignSchedulerUpdationDto>(scheduler));
            }
        }

    }
}