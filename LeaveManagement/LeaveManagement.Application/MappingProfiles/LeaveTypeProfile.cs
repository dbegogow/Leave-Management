namespace LeaveManagement.Application.MappingProfiles;

using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using LeaveManagement.Domain;

using AutoMapper;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        this.CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        this.CreateMap<LeaveType, LeaveTypeDetailsDto>();
    }
}
