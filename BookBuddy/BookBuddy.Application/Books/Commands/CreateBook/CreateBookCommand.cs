using BookBuddy.Domain.BookAggregate;
using MediatR;

namespace BookBuddy.Application.Books.Commands.CreateBook;

public record CreateBookCommand(
    string Title,
    AuthorCommand Author,
    PublisherCommand Publisher,
    BookFormatCommand BookFormat,
    ProgrammingLanguageCommand ProgrammingLanguage,
    string? Isbn,
    int PublicationYear,
    string? Genre,
    string? Website,
    string? Notes) : IRequest<Book>;

public record class ProgrammingLanguageCommand(
       string Language);

public record class BookFormatCommand(
       string Format);

public record AuthorCommand(
    string FirstName,
    string LastName);

public record PublisherCommand(
    string Name,
    string? Website);