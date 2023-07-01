using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate;
using MediatR;

namespace BookBuddy.Application.Books.Queries.GetBook;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookAsync(request.Id);
        return book;
    }
}
