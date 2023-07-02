namespace BookBuddy.Contracts.Books;

public record BookResponse(
    int Id,
    string Title,
    AuthorResponse Author,
    PublisherResponse Publisher,
    BookFormatResponse BookFormat,
    ProgrammingLanguageResponse ProgrammingLanguage,
    string? Isbn,
    int PublicationYear,
    string? Genre,
    string? Website,
    string? Notes,
    DateTime Datecreate);

public record class ProgrammingLanguageResponse(
    int ProgrammingLanguageId,
    string Language);

public record class BookFormatResponse(
    int BookFormatId,
    string Format);

public record AuthorResponse(
    int AuthorId,
    string FirstName,
    string LastName);

public record PublisherResponse(
    int PublisherId,
    string Name,
    string? Website);