namespace LeaveManagement.Application.Features.LeaveRequest.Shared;

public record BaseLeaveRequest(
    DateTime StartDate,
    DateTime EndDate,
    int LeaveTypeId);
