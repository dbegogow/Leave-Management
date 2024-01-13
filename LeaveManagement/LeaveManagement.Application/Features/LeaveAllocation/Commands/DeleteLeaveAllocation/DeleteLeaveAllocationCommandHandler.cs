namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;

using MediatR;

public class DeleteLeaveAllocationCommandHandler
    : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository leaveAllocationRepository;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        => this.leaveAllocationRepository = leaveAllocationRepository;

    public async Task<Unit> Handle(
        DeleteLeaveAllocationCommand request,
        CancellationToken cancellationToken)
    {
        var leaveAllocation = await this.leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
        {
            throw new NotFoundException(nameof(leaveAllocation), request.Id);
        }

        await this.leaveAllocationRepository.DeleteAsync(leaveAllocation);

        return Unit.Value;
    }
}
