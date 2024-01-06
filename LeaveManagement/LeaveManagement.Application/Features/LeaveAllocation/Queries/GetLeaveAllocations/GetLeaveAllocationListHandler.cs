namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

using LeaveManagement.Application.Contracts.Persistence;

using AutoMapper;
using MediatR;

public class GetLeaveAllocationListHandler : IRequestHandler<GetLeaveAllocationListQuery, IEnumerable<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    private readonly IMapper mapper;

    public GetLeaveAllocationListHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper)
    {
        this.leaveAllocationRepository = leaveAllocationRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<LeaveAllocationDto>> Handle(
        GetLeaveAllocationListQuery request,
        CancellationToken cancellationToken)
    {
        var leaveAllocations = await this.leaveAllocationRepository
            .GetLeaveAllocationsWithDetails();

        return this.mapper.Map<IEnumerable<LeaveAllocationDto>>(leaveAllocations);
    }
}
