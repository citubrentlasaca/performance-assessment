using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class PeerAssessmentItemMappings : Profile
    {
        public PeerAssessmentItemMappings()
        {
            CreateMap<PeerAssessmentItemCreationDto, PeerAssessmentItem>(MemberList.None)
                .ForMember(dto => dto.Question, opt => opt.MapFrom(st => (st.Question!)))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(st => (st.QuestionType!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)))
                .ForMember(dto => dto.Target, opt => opt.MapFrom(st => (st.Target!)))
                .ForMember(dto => dto.Required, opt => opt.MapFrom(st => (st.Required!)))
                .ForMember(dto => dto.PeerAssessmentId, opt => opt.MapFrom(st => (st.PeerAssessmentId!)));

            CreateMap<PeerAssessmentItemUpdationDto, PeerAssessmentItem>(MemberList.None)
                .ForMember(dto => dto.Question, opt => opt.MapFrom(st => (st.Question!)))
                .ForMember(dto => dto.QuestionType, opt => opt.MapFrom(st => (st.QuestionType!)))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(st => (st.Weight!)))
                .ForMember(dto => dto.Target, opt => opt.MapFrom(st => (st.Target!)))
                .ForMember(dto => dto.Required, opt => opt.MapFrom(st => (st.Required!)));
        }
    }
}
