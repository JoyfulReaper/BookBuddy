using BookBuddy.Shared.Enums;

namespace BookBuddy.Shared.Contracts;

public record UpdateBookRequest(int Id,
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