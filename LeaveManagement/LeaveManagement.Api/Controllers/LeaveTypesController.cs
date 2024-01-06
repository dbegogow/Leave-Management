namespace LeaveManagement.Api.Controllers
{
    using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
    using LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
    using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
    using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
    using LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

    using Microsoft.AspNetCore.Mvc;

    using MediatR;

    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveTypesController(IMediator mediator)
            => this.mediator = mediator;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LeaveTypeDto>>> Get()
            => Ok(await this.mediator.Send(new GetLeaveTypesQuery()));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
            => Ok(await this.mediator.Send(new GetLeaveTypeDetailsQuery(id)));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveType)
        {
            var response = await this.mediator.Send(leaveType);

            return CreatedAtAction(nameof(Get), new { Id = response });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(UpdateLeaveTypeCommand leaveType)
        {
            await this.mediator.Send(leaveType);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await this.mediator.Send(new DeleteLeaveTypeCommand(id));

            return NoContent();
        }
    }
}
