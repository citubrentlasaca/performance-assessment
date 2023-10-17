using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class EmployeeAnnouncementNotificationMappings : Profile
    {
        public EmployeeAnnouncementNotificationMappings() 
        {
            CreateMap<EmployeeAnnouncementNotificationCreationDto, EmployeeAnnouncementNotification>(MemberList.None)
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(st => (st.EmployeeId!)))
                .ForMember(dto => dto.AnnouncementId, opt => opt.MapFrom(st => (st.AnnouncementId!)))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
