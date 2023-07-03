using BookBuddy.Domain.BookAggregate;
using MediatR;

namespace BookBuddy.Application.Books.Commands.CreateBook;

public record UpdateBookCommand(
    int BookId,
    string Title,
    int? AuthorId,
    int? PublisherId,
    int? BookFormatId,
    int? ProgrammingLanguageId,
    string? Isbn,
    int PublicationYear,
    string? Genre,
    string? Website,
    string? Notes) : IRequest<Book>;