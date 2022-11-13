using ECourses.ApplicationCore.Features.Commands.Authors;
using ECourses.ApplicationCore.Features.Queries.Authors;
using ECourses.Web.Attributes;
using ECourses.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECourses.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var author = await _mediator.Send(new GetAuthorQuery() { Id = id });

            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(CreateAuthorCommand command)
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
            await _mediator.Send(new DeleteAuthorCommand { Id = id });

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(UpdateAuthorCommand command)
        {
            await _mediator.Send(command);

            return Ok(command);
        }

        [HttpGet("Paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaged([FromQuery] GetAllAuthorsPagedQuery query)
        {
            var authors = await _mediator.Send(query);

            return Ok(authors);
        }
    }
}
