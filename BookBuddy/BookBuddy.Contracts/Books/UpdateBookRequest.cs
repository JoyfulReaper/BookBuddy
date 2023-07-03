namespace BookBuddy.Contracts.Books;

public record UpdateBookRequest(
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
    string? Notes);