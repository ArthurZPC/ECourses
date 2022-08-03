﻿using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;
using ECourses.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECourses.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesErrorResponseType(typeof(ErrorDetails))]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAuthors();

            return Ok(authors);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var author = await _authorService.GetAuthorById(id);

            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateAuthorViewModel model)
        {
            await _authorService.Create(model);

            return CreatedAtAction(nameof(Create), model);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _authorService.Delete(id);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateAuthorViewModel model)
        {
            await _authorService.Update(model);

            return Ok(model);
        }

        [HttpGet("Paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaged([FromQuery] PaginationQuery paginationQuery, string? order, [FromQuery] AuthorFilterQuery? filterQuery)
        {
            var authors = await _authorService.GetPagedList(paginationQuery, order, filterQuery);

            return Ok(authors);
        }
    }
}
