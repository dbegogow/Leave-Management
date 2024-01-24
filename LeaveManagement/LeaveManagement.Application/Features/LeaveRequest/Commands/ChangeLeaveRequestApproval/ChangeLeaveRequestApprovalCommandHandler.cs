namespace LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

using AutoMapper;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using LeaveManagement.Application.Logging;
using LeaveManagement.Application.Models.Email;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;
    private readonly IEmailSender emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;

    public ChangeLeaveRequestApprovalCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IEmailSender emailSender,
        IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        this.leaveRequestRepository = leaveRequestRepository;
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
        this.emailSender = emailSender;
        this.appLogger = appLogger;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await this.leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }

        leaveRequest.Approved = request.Approved;

        await this.leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, // Get email from employee record
                Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                Subject = "Leave Request Approval Status Updated"
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
