using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class ResultMappings : Profile
    {
        public ResultMappings()
        {
            CreateMap<ResultCreationDto, Result>(MemberList.None)
                .ForMember(dto => dto.AssessmentId, opt => opt.MapFrom(st => (st.AssessmentId!)))
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(st => (st.EmployeeId!)))
                .ForMember(dto => dto.DateTimeDue, opt => opt.MapFrom(st => (st.DateTimeDue!)))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<ResultUpdationDto, Result>(MemberList.None)
                .ForMember(dto => dto.AssessmentId, opt => opt.MapFrom(st => (st.AssessmentId!)))
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(st => (st.EmployeeId!)))
                .ForMember(dto => dto.Score, opt => opt.MapFrom(st => (st.Score!)))
                .ForMember(dto => dto.DateTimeDue, opt => opt.MapFrom(st => (st.DateTimeDue!)))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
