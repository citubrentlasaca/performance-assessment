using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class PeerAssessmentChoiceMappings : Profile
    {
        public PeerAssessmentChoiceMappings()
        {
            CreateMap<PeerAssessmentChoiceCreationDto, PeerAssessmentChoice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)))
                .ForMember(dto => dto.PeerAssessmentItemId, opt => opt.MapFrom(st => (st.PeerAssessmentItemId!)));

            CreateMap<PeerAssessmentChoiceUpdationDto, PeerAssessmentChoice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)));
        }
    }
}
