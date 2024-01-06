namespace LeaveManagement.Api.Controllers;

using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator mediator;

    public LeaveAllocationsController(IMediator mediator)
        => this.mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        => Ok(await this.mediator.Send(new GetLeaveAllocationListQuery()));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        => Ok(await this.mediator.Send(new GetLeaveAllocationDetailsQuery(id)));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await this.mediator.Send(leaveAllocation);

        return CreatedAtAction(nameof(Get), new { Id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await this.mediator.Send(leaveAllocation);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await this.mediator.Send(new DeleteLeaveAllocationCommand(id));

        return NoContent();
    }
}
