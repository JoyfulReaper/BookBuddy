namespace BookBuddy.Contracts.Books;

public record BookResponse(
    int Id,
    string Title,
    AuthorResponse Author,
    PublisherResponse Publisher,
    BookFormatResponse BookFormat,
    ProgrammingLanguageResponse ProgrammingLanguage,
    string? Ibsn,
    int PublicationYear,
    string? Genre,
    string? Website,
    string? Notes,
    DateTime Datecreate);

public record class ProgrammingLanguageResponse(
    string Language);

public record class BookFormatResponse(
    string Format);

public record AuthorResponse(
    string FirstName,
    string LastName);

public record PublisherResponse(
    string Name,
    string? Website);