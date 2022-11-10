using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Features.Queries.Videos;
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
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly IMediator _mediator;

        public VideoController(IVideoService videoService, IMediator mediator)
        {
            _videoService = videoService;
            _mediator = mediator;
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var video = await _mediator.Send(new GetVideoQuery { Id = id });

            return Ok(video);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromForm]CreateVideoViewModel model)
        {
            await _videoService.Create(model);

            return CreatedAtAction(nameof(Create), model);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _videoService.Delete(id);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update([FromForm]UpdateVideoViewModel model)
        {
            await _videoService.Update(model);

            return Ok(model);
        }

        [HttpGet("Paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaged([FromQuery] GetAllVideosPagedQuery query)
        {
            var videos = await _mediator.Send(query);

            return Ok(videos);
        }
    }
}
