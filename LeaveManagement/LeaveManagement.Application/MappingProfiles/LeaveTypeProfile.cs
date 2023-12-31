﻿namespace LeaveManagement.Application.MappingProfiles;

using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using LeaveManagement.Domain;

using AutoMapper;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        this.CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        this.CreateMap<LeaveType, LeaveTypeDetailsDto>();
        this.CreateMap<CreateLeaveTypeCommand, LeaveType>();
        this.CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
}
