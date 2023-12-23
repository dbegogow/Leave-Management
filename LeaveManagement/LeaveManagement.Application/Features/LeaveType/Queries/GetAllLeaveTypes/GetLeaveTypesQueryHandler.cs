namespace LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;

using AutoMapper;
using MediatR;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, IEnumerable<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;

    public GetLeaveTypesQueryHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<LeaveTypeDto>> Handle(
        GetLeaveTypesQuery request,
        CancellationToken cancellationToken)
    {
        var leaveTypes = await this.leaveTypeRepository.GetAsync();

        var data = this.mapper.Map<IEnumerable<LeaveTypeDto>>(leaveTypes);

        return data;
    }
}
