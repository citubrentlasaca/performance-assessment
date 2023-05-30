using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class ItemMappings : Profile
    {
        public ItemMappings()
        {
            CreateMap<ItemCreationDto, Item>(MemberList.None)
                .ForMember(dto => dto.Question, opt => opt.MapFrom(st => (st.Question!)))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(st => (st.QuestionType!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)))
                .ForMember(dto => dto.Required, opt => opt.MapFrom(st => (st.Required!)))
                .ForMember(dto => dto.AssessmentId, opt => opt.MapFrom(st => (st.AssessmentId!)));

            CreateMap<ItemUpdationDto, Item>(MemberList.None)
                .ForMember(dto => dto.Question, opt => opt.MapFrom(st => (st.Question!)))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(st => (st.QuestionType!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)))
                .ForMember(dto => dto.Required, opt => opt.MapFrom(st => (st.Required!)));
        }
    }
}
