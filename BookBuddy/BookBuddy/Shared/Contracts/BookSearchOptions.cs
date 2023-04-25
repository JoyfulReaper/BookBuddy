using BookBuddy.Shared.Enums;

namespace BookBuddy.Shared.Contracts;

public class BookSearchOptions
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? ISBN { get; set; }
    public int? PublicationYear { get; set; }
    public string? Genre { get; set; }
    public BookFormat? Format { get; set; }
    public ProgrammingLanguage? ProgrammingLanguage { get; set; }
}
