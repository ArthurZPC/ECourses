using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Features.Queries.Ratings;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Web.Attributes;
using ECourses.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECourses.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly IMediator _mediator;

        public RatingController(IRatingService ratingService, IMediator mediator)
        {
            _ratingService = ratingService;
            _mediator = mediator;
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rating = await _mediator.Send(new GetRatingQuery { Id = id });

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(CreateRatingViewModel model)
        {
            await _ratingService.Create(model);

            return CreatedAtAction(nameof(Create), model);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ratingService.Delete(id);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(UpdateRatingViewModel model)
        {
            await _ratingService.Update(model);

            return Ok(model);
        }

        [HttpGet("Paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaged([FromQuery] GetAllRatingsPagedQuery query)
        {
            var ratings = await _mediator.Send(query);

            return Ok(ratings);
        }
    }
}
