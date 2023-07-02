using BookBuddy.Application.Books.Commands.CreateBook;
using BookBuddy.Application.Books.Queries.GetBook;
using BookBuddy.Contracts.Books;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBuddy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public BookController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponse>> GetBook(int id)
    {
        var query = new GetBookQuery(BookId.Create(id));
        var book = await _mediator.Send(query);

        if(book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse>> CreateBook(CreateBookRequest request)
    {
        var command = _mapper.Map<CreateBookCommand>(request);
        var createdBook = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, _mapper.Map<BookResponse>(createdBook));
    }
}
