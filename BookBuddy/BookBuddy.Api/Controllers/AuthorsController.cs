using BookBuddy.Application.Authors.Queries.GetAllAuthors;
using BookBuddy.Application.Authors.Queries.GetAuthor;
using BookBuddy.Application.Common.Exceptions;
using BookBuddy.Contracts.Books;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBuddy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public AuthorsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorResponse>>> GetAll()
    {
        var command = new GetAllAuthorsQuery();
        var authors = await _mediator.Send(command);

        return Ok(_mapper.Map<IEnumerable<AuthorResponse>>(authors));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorResponse>> Get(int id)
    {
        try
        {
            var command = new GetAuthorQuery(AuthorId.Create(id));
            var author = await _mediator.Send(command);

            return Ok(_mapper.Map<AuthorResponse>(author));
        }
        catch (AuthorNotFoundException)
        {
            return NotFound();
        }
    }
}