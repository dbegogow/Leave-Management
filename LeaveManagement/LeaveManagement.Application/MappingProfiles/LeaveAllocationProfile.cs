namespace LeaveManagement.Application.MappingProfiles;

using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using LeaveManagement.Domain;

using AutoMapper;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        this.CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
        this.CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
        this.CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        this.CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}
