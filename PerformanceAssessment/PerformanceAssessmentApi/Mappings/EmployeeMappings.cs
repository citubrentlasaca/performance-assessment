using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class EmployeeMappings : Profile
    {
        public EmployeeMappings()
        {
            CreateMap<EmployeeCreationDto, Employee>(MemberList.None)
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(st => (st.UserId!)))
                .ForMember(dto => dto.TeamId, opt => opt.MapFrom(st => (st.TeamId!)))
                .ForMember(dto => dto.DateTimeJoined, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<EmployeeTeamInfoDto, Employee>(MemberList.None)
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(st => (st.UserId!)))
                .ForMember(dto => dto.Role, opt => opt.MapFrom(st => (st.Role!)))
                .ForMember(dto => dto.DateTimeJoined, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<EmployeeUpdationDto, Employee>(MemberList.None)
                .ForMember(dto => dto.Status, opt => opt.MapFrom(st => (st.Status!)));
        }
    }
}
