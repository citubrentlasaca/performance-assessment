using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;

namespace PerformanceAssessmentApi.Mappings
{
    public class EmployerAssessmentChoiceMappings : Profile
    {
        public EmployerAssessmentChoiceMappings()
        {
            CreateMap<EmployerAssessmentChoiceCreationDto, EmployerAssessmentChoice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)))
                .ForMember(dto => dto.EmployerAssessmentItemId, opt => opt.MapFrom(st => (st.EmployerAssessmentItemId!)));

            CreateMap<EmployerAssessmentChoiceUpdationDto, EmployerAssessmentChoice>(MemberList.None)
                .ForMember(dto => dto.ChoiceValue, opt => opt.MapFrom(st => (st.ChoiceValue!)));
        }
    }
}
