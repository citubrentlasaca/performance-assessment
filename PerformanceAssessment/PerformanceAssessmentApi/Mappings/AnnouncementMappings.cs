using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class AnnouncementMappings : Profile
    {
        public AnnouncementMappings()
        {
            CreateMap<AnnouncementCreationDto, Announcement>(MemberList.None)
                .ForMember(dto => dto.TeamId, opt => opt.MapFrom(st => (st.TeamId!)))
                .ForMember(dto => dto.Content, opt => opt.MapFrom(st => (st.Content!)))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<AnnouncementUpdationDto, Announcement>(MemberList.None)
                .ForMember(dto => dto.Content, opt => opt.MapFrom(st => (st.Content!)))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
