using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class PeerAssessmentMappings : Profile
    {
        public PeerAssessmentMappings()
        {
            CreateMap<PeerAssessmentCreationDto, PeerAssessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));

            CreateMap<PeerAssessmentUpdationDto, PeerAssessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)));
        }
    }
}
