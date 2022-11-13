using ECourses.ApplicationCore.Features.Commands.Tags;
using ECourses.ApplicationCore.Features.Queries.Tags;
using ECourses.Web.Attributes;
using ECourses.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECourses.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await _mediator.Send(new GetTagQuery { Id = id });

            return Ok(tag);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(CreateTagCommand command)
        {
            await _mediator.Send(command);

            return CreatedAtAction(nameof(Create), command);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTagCommand { Id = id });

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(UpdateTagCommand command)
        {
            await _mediator.Send(command);

            return Ok(command);
        }

        [HttpGet("Paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaged([FromQuery] GetAllTagsPagedQuery query)
        {
            var tags = await _mediator.Send(query);

            return Ok(tags);
        }
    }
}
