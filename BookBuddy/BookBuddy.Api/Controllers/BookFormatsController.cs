using BookBuddy.Application.BookFormats.Queries.GetAllBookFormats;
using BookBuddy.Application.BookFormats.Queries.GetBookFormat;
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
public class BookFormatsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public BookFormatsController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookFormatResponse>>> GetAllFormats()
    {
        var query = new GetAllBookFormatsQuery();
        var formats = await _sender.Send(query);

        return Ok(_mapper.Map<IEnumerable<BookFormat>>(formats));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookFormatResponse>> GetFormat(int id)
    {
        try
        {
            var query = new GetBookFormat(BookFormatId.Create(id));
            var format = await _sender.Send(query);

            return Ok(_mapper.Map<BookFormatResponse>(format));
        }
        catch (BookFormatNotFoundException)
        {
            return NotFound();
        }
    }
}
