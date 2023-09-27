using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class SelfAssessmentMappings : Profile
    {
        public SelfAssessmentMappings()
        {
            CreateMap<SelfAssessmentCreationDto, SelfAssessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));

            CreateMap<SelfAssessmentUpdationDto, SelfAssessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));
        }
    }
}
