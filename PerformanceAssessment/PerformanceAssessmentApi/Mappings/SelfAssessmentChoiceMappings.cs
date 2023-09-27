using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class SelfAssessmentChoiceMappings : Profile
    {
        public SelfAssessmentChoiceMappings()
        {
            CreateMap<SelfAssessmentChoiceCreationDto, SelfAssessmentChoice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)))
                .ForMember(dto => dto.SelfAssessmentItemId, opt => opt.MapFrom(st => (st.SelfAssessmentItemId!)));

            CreateMap<SelfAssessmentChoiceUpdationDto, SelfAssessmentChoice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)));
        }
    }
}
