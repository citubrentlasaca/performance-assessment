using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class SelfAssessmentItemMappings : Profile
    {
        public SelfAssessmentItemMappings()
        {
            CreateMap<SelfAssessmentItemCreationDto, SelfAssessmentItem>(MemberList.None)
                .ForMember(dto => dto.Question, opt => opt.MapFrom(st => (st.Question!)))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(st => (st.QuestionType!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)))
                .ForMember(dto => dto.Target, opt => opt.MapFrom(st => (st.Target!)))
                .ForMember(dto => dto.Required, opt => opt.MapFrom(st => (st.Required!)))
                .ForMember(dto => dto.SelfAssessmentId, opt => opt.MapFrom(st => (st.SelfAssessmentId!)));

            CreateMap<SelfAssessmentItemUpdationDto, SelfAssessmentItem>(MemberList.None)
                .ForMember(dto => dto.Question, opt => opt.MapFrom(st => (st.Question!)))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(st => (st.QuestionType!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)))
                .ForMember(dto => dto.Target, opt => opt.MapFrom(st => (st.Target!)))
                .ForMember(dto => dto.Required, opt => opt.MapFrom(st => (st.Required!)));
        }
    }
}
