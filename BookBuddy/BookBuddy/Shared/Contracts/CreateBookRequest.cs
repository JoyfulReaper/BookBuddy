using BookBuddy.Shared.Enums;

namespace BookBuddy.Shared.Contracts;

public record CreateBookRequest(
    string Title,
    string Author,
    string? Publisher,
    string? ISBN,
    int PublicationYear,
    string? Genre,
    BookFormat Format,
    string? Notes,
    ProgrammingLanguage? ProgrammingLanguage
);