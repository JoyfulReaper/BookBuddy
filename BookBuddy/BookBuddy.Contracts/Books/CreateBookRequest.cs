namespace BookBuddy.Contracts.Books;

public record CreateBookRequest(
    string Title,
    Author Author,
    Publisher Publisher,
    BookFormat BookFormat,
    ProgrammingLanguage ProgrammingLanguage,
    string? Isbn,
    int PublicationYear,
    string? Genre,
    string? Website,
    string? Notes);

public record class ProgrammingLanguage(
       string Language);

public record class BookFormat(
       string Format);

public record Author (
    string FirstName,
    string LastName);

public record Publisher (
    string Name,
    string? Website);