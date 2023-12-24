namespace LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;

using MediatR;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        => this.leaveTypeRepository = leaveTypeRepository;

    public async Task<Unit> Handle(
        DeleteLeaveTypeCommand request,
        CancellationToken cancellationToken)
    {
        var leaveTypeToDelete = await this.leaveTypeRepository.GetByIdAsync(request.Id);

        await this.leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        return Unit.Value;
    }
}
