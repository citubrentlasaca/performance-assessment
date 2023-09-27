using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class EmployerAssessmentMappings : Profile
    {
        public EmployerAssessmentMappings()
        {
            CreateMap<EmployerAssessmentCreationDto, EmployerAssessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));

            CreateMap<EmployerAssessmentUpdationDto, EmployerAssessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));
        }
    }
}
