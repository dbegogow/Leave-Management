namespace LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Logging;

using AutoMapper;
using MediatR;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, IEnumerable<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> logger;

    public GetLeaveTypesQueryHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<IEnumerable<LeaveTypeDto>> Handle(
        GetLeaveTypesQuery request,
        CancellationToken cancellationToken)
    {
        var leaveTypes = await this.leaveTypeRepository.GetAsync();

        var data = this.mapper.Map<IEnumerable<LeaveTypeDto>>(leaveTypes);

        this.logger.LogInformation("Leave types were retrieved successfully");

        return data;
    }
}
