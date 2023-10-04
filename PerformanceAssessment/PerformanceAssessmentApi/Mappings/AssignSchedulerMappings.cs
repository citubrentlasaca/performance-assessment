﻿using AutoMapper;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Models;
using PerformanceAssessmentApi.Utils;

namespace PerformanceAssessmentApi.Mappings
{
    public class AssignSchedulerMappings : Profile
    {
        public AssignSchedulerMappings()
        {
            CreateMap<AssignSchedulerCreationDto, AssignScheduler>(MemberList.None)
                .ForMember(dto => dto.AssessmentId, opt => opt.MapFrom(st => (st.AssessmentId!)))
                .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(st => (st.EmployeeId!)))
                .ForMember(dto => dto.Reminder, opt => opt.MapFrom(st => (st.Reminder!)))
                .ForMember(dto => dto.Occurrence, opt => opt.MapFrom(st => (st.Occurrence!)))
                .ForMember(dto => dto.DueDate, opt => opt.MapFrom(st => st.DueDate))
                .ForMember(dto => dto.Time, opt => opt.MapFrom(st => st.Time))
                .ForMember(dto => dto.DateTimeCreated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));

            CreateMap<AssignSchedulerUpdationDto, AssignScheduler>(MemberList.None)
                .ForMember(dto => dto.Reminder, opt => opt.MapFrom(st => (st.Reminder!)))
                .ForMember(dto => dto.Occurrence, opt => opt.MapFrom(st => (st.Occurrence!)))
                .ForMember(dto => dto.DueDate, opt => opt.MapFrom(st => st.DueDate))
                .ForMember(dto => dto.Time, opt => opt.MapFrom(st => st.Time))
                .ForMember(dto => dto.DateTimeUpdated, opt => opt.MapFrom(st => StringUtil.GetCurrentDateTime()));
        }
    }
}