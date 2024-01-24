namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

using System.Threading;
using System.Threading.Tasks;

using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using LeaveManagement.Application.Logging;
using LeaveManagement.Application.Models.Email;

using MediatR;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly IEmailSender emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;

    public CancelLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        this.leaveRequestRepository = leaveRequestRepository;
        this.emailSender = emailSender;
        this.appLogger = appLogger;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await this.leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }

        leaveRequest.Cancelled = true;

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // Get email from employee record
                Body = $"Your leave request for {leaveRequest.StartDate} to {leaveRequest.EndDate:D} has beed cancelled successfully",
                Subject = "Leave Request Cancelled"
            };

            await this.emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            this.appLogger.LogWarning(ex.Message);
        }

        return Unit.Value;
    }
}
