using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserCreationDto, UserDto>(MemberList.None)
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(st => (st.FirstName!)))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(st => (st.LastName!)))
                .ForMember(dto => dto.Role, opt => opt.MapFrom(st => (st.Role!)))
                .ForMember(dto => dto.EmailAddress, opt => opt.MapFrom(st => (st.EmailAddress!)))
                .ForMember(dto => dto.Password, opt => opt.MapFrom(st => (st.Password!)))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<UserUpdationDto, User>(MemberList.None)
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(st => (st.FirstName!)))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(st => (st.LastName!)))
                .ForMember(dto => dto.EmailAddress, opt => opt.MapFrom(st => (st.EmailAddress!)))
                .ForMember(dto => dto.Password, opt => opt.MapFrom(st => (st.Password!)))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}
