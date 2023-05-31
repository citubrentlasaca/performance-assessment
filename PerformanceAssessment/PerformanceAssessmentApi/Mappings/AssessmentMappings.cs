using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class AssessmentMappings : Profile
    {
        public AssessmentMappings()
        {
            CreateMap<AssessmentCreationDto, Assessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));

            CreateMap<AssessmentUpdationDto, Assessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));
        }
    }
}
