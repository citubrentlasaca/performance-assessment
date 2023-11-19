using AutoMapper;
using PerformanceAssessmentApi.Utils;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class AnswerMappings : Profile
    {
        public AnswerMappings()
        {
            CreateMap<AnswerCreationDto, Answer>(MemberList.None)
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(st => (st.EmployeeId!)))
                .ForMember(dto => dto.ItemId, opt => opt.MapFrom(st => (st.ItemId!)))
                .ForMember(dto => dto.AnswerText, opt => opt.MapFrom(st => (st.AnswerText!)))
                .ForMember(dto => dto.SelectedChoices, opt => opt.MapFrom(st => (st.SelectedChoices!)))
                .ForMember(dto => dto.CounterValue, opt => opt.MapFrom(st => (st.CounterValue!)))
                .ForMember(dto => dto.DateTimeAnswered, opt => opt.MapFrom(st => (StringUtil.GetCurrentDateTime())));

            CreateMap<AnswerUpdationDto, Answer>(MemberList.None)
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(st => (st.EmployeeId!)))
                .ForMember(dto => dto.ItemId, opt => opt.MapFrom(st => (st.ItemId!)))
                .ForMember(dto => dto.AnswerText, opt => opt.MapFrom(st => (st.AnswerText!)))
                .ForMember(dto => dto.SelectedChoices, opt => opt.MapFrom(st => (st.SelectedChoices!)))
                .ForMember(dto => dto.CounterValue, opt => opt.MapFrom(st => (st.CounterValue!)));
        }
    }
}
