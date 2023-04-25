using AutoMapper;
using BookBuddy.Server.Data;
using BookBuddy.Server.Services.Interfaces;
using BookBuddy.Shared.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBuddy.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BooksController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponse>> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<BookResponse>(book));
    }

    [HttpGet]
    public async Task<ActionResult<List<BookResponse>>> GetBooks()
    {
        var books = await _bookService.GetBooksAsync();
        return Ok(_mapper.Map<List<BookResponse>>(books));
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse>> CreateBook(CreateBookRequest createBookRequest)
    {
        var book = _mapper.Map<Book>(createBookRequest);
        var createdBook = await _bookService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, _mapper.Map<BookResponse>(createdBook));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BookResponse>> UpdateBook(int id, CreateBookRequest createBookRequest)
    {
        var book = _mapper.Map<Book>(createBookRequest);
        book.Id = id;
        var updatedBook = await _bookService.UpdateBookAsync(book);
        if (updatedBook == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<BookResponse>(updatedBook));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("search")]
    public async Task<ActionResult<List<BookResponse>>> SearchBooks(BookSearchOptions bookSearchOptions)
    {
        var books = await _bookService.SearchBooksAsync(bookSearchOptions);
        return Ok(_mapper.Map<List<BookResponse>>(books));
    }
}
