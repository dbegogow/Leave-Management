namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;

using AutoMapper;
using MediatR;

public class GetLeaveAllocationDetailsRequestHandler
    : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    private readonly IMapper mapper;

    public GetLeaveAllocationDetailsRequestHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper)
    {
        this.leaveAllocationRepository = leaveAllocationRepository;
        this.mapper = mapper;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await this.leaveAllocationRepository
            .GetLeaveAllocationWithDetails(request.Id);

        return this.mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}
