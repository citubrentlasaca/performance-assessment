using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class AssessmentMappings : Profile
    {
        public AssessmentMappings()
        {
            CreateMap<AssessmentCreationDto, Assessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<AssessmentUpdationDto, Assessment>(MemberList.None)
                .ForMember(dto => dto.Title, opt => opt.MapFrom(st => (st.Title!)))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(st => (st.Description!)))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
