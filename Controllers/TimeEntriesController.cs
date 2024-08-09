using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TimeEntryApproval.API.Application.Commands.CreateTimeEntry;
using TimeEntryApproval.API.Application.Models.Requests;

namespace TimeEntryApproval.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeEntriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateTimeEntryRequest request)
        {
            var result = await _mediator.Send(new CreateTimeEntryCommand() 
                {
                    Date = request.Date,
                    Start = request.Start,
                    End = request.End,
                    TaskId = request.TaskId,
                }
            );

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
