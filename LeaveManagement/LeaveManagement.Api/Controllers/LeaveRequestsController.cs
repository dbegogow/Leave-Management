namespace LeaveManagement.Api.Controllers;

using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator mediator;

    public LeaveRequestsController(IMediator mediator)
        => this.mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LeaveRequestListDto>>> Get()
        => Ok(await this.mediator.Send(new GetLeaveRequestListQuery()));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        => Ok(await this.mediator.Send(new GetLeaveRequestDetailsQuery(id)));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveType)
    {
        var response = await this.mediator.Send(leaveType);

        return CreatedAtAction(nameof(Get), new { Id = response });
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
    {
        await this.mediator.Send(leaveRequest);

        return NoContent();
    }

    [HttpPut]
    [Route("cancel-request")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
    {
        await this.mediator.Send(cancelLeaveRequest);

        return NoContent();
    }

    [HttpPut]
    [Route("update-approval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
    {
        await this.mediator.Send(updateApprovalRequest);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await this.mediator.Send(new DeleteLeaveRequestCommand(id));

        return NoContent();
    }
}
