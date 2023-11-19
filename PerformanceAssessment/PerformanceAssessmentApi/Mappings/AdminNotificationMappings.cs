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
                .ForMember(dto => dto.EmployeeName, opt => opt.MapFrom(st => (st.EmployeeName!)))
                .ForMember(dto => dto.AssessmentTitle, opt => opt.MapFrom(st => (st.AssessmentTitle!)))
                .ForMember(dto => dto.TeamName, opt => opt.MapFrom(st => (st.TeamName!)))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
