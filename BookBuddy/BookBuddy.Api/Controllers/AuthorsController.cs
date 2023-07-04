using BookBuddy.Application.Authors.Queries.GetAllAuthors;
using BookBuddy.Contracts.Books;
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
    public async Task<ActionResult<IEnumerable<Author>>> GetAll()
    {
        var command = new GetAllAuthorsQuery();
        var authors = await _mediator.Send(command);

        return Ok(_mapper.Map<IEnumerable<Author>>(authors));
    }
}