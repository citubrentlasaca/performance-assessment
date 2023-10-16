using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class TeamMappings : Profile
    {
        public TeamMappings()
        {
            CreateMap<TeamCreationDto, Team>(MemberList.None)
                .ForMember(dto => dto.Organization, opt => opt.MapFrom(st => (st.Organization!)))
                .ForMember(dto => dto.BusinessType, opt => opt.MapFrom(st => (st.BusinessType!)))
                .ForMember(dto => dto.BusinessAddress, opt => opt.MapFrom(st => (st.BusinessAddress!)))
                .ForMember(dto => dto.TeamCode, opt => opt.Ignore())
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<TeamUpdationDto, Team>(MemberList.None)
                .ForMember(dto => dto.Organization, opt => opt.MapFrom(st => (st.Organization!)))
                .ForMember(dto => dto.BusinessType, opt => opt.MapFrom(st => (st.BusinessType!)))
                .ForMember(dto => dto.BusinessAddress, opt => opt.MapFrom(st => (st.BusinessAddress!)))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
