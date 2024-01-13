namespace LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;

using MediatR;

public class DeleteLeaveRequestCommandHandler
    : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        => this.leaveRequestRepository = leaveRequestRepository;

    public async Task<Unit> Handle(
        DeleteLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        var leaveRequest = await this.leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        await this.leaveRequestRepository.DeleteAsync(leaveRequest);

        return Unit.Value;
    }
}
