using BookBuddy.Application.Books.Commands.CreateBook;
using BookBuddy.Application.Common.Exceptions;
using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using MediatR;

namespace BookBuddy.Application.Books.Commands.UpdateBook;
public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var bookExists = await _bookRepository.BookExists(BookId.Create(request.BookId));
        if(bookExists == false)
            throw new BookNotFoundException($"Unable to look up book with id: {request.BookId}");

        var updatedBook = Book.Create(BookId.Create(request.BookId),
            request.Title,
            request.AuthorId == null ? null : AuthorId.Create(request.AuthorId.Value),
            request.PublisherId == null ? null : PublisherId.Create(request.PublisherId.Value),
            request.BookFormatId == null ? null : BookFormatId.Create(request.BookFormatId.Value),
            request.ProgrammingLanguageId == null ? null : ProgrammingLanguageId.Create(request.ProgrammingLanguageId.Value),
            null,
            null,
            null,
            null,
            request.Isbn,
            request.PublicationYear,
            request.Genre,
            request.Website,
            request.Notes,
            DateTime.Now);


        await _bookRepository.UpdateBookAsync(updatedBook);
        return await _bookRepository.GetBookAsync(updatedBook.Id);
    }
}
