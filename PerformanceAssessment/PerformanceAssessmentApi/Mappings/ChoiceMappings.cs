using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class ChoiceMappings : Profile
    {
        public ChoiceMappings()
        {
            CreateMap<ChoiceCreationDto, Choice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)))
                .ForMember(dto => dto.ItemId, opt => opt.MapFrom(st => (st.ItemId!)));

            CreateMap<ChoiceUpdationDto, Choice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)));
        }
    }
}
