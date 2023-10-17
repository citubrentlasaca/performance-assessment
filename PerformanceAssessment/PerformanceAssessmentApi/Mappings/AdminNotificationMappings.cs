using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class AdminNotificationMappings : Profile
    {
        public AdminNotificationMappings()
        {
            CreateMap<AdminNotificationCreationDto, AdminNotification>(MemberList.None)
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(st => (st.EmployeeId!)))
                .ForMember(dto => dto.AssessmentId, opt => opt.MapFrom(st => (st.AssessmentId!)))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
