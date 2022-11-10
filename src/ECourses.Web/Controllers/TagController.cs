using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Features.Queries.Tags;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;
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
        private readonly ITagService _tagService;
        private readonly IMediator _mediator;

        public TagController(ITagService tagService, IMediator mediator)
        {
            _tagService = tagService;
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
        public async Task<IActionResult> Create(CreateTagViewModel model)
        {
            await _tagService.Create(model);

            return CreatedAtAction(nameof(Create), model);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tagService.Delete(id);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(UpdateTagViewModel model)
        {
            await _tagService.Update(model);

            return Ok(model);
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
