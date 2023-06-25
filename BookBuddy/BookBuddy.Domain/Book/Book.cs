using BookBuddy.Domain.Author;
using BookBuddy.Domain.BookFormat;
using BookBuddy.Domain.Common;
using BookBuddy.Domain.ProgrammingLanguage;
using BookBuddy.Domain.Publiser;

namespace BookBuddy.Domain.Book;

public class Book : Entity<BookId>
{
    private Book(BookId bookId,
        string title,
        AuthorId? authorId,
        PublisherId? publisherId,
        BookFormatId? bookFormat,
        ProgrammingLanguageId? programmingLanguageId,
        string? isbn,
        int publicationYear,
        string? genre,
        string? website,
        string? notes,
        DateTime datecreated) : base (bookId)
    {
        BookId = bookId;
        Title = title;
        AuthorId = authorId;
        PublisherId = publisherId;
        BookFormatId = bookFormat;
        ProgrammingLanguageId = programmingLanguageId;
        Isbn = isbn;
        PublicationYear = publicationYear;
        Genre = genre;
        Website = website;
        Notes = notes;
        DateCreated = datecreated;
    }

    public static Book Create(BookId bookId,
        string title,
        AuthorId? authorId,
        PublisherId? publisherId,
        BookFormatId? bookFormatId,
        ProgrammingLanguageId? programmingLanguageId,
        string? isbn,
        int publicationYear,
        string? genre,
        string? website,
        string? notes,
        DateTime dateCreated)
    {
        return new Book(bookId,
            title,
            authorId,
            publisherId,
            bookFormatId,
            programmingLanguageId,
            isbn,
            publicationYear,
            genre,
            website,
            notes,
            dateCreated);
    }

    public BookId BookId { get; }
    public string Title { get; } = default!;
    public AuthorId? AuthorId { get; }
    public PublisherId? PublisherId { get; }
    public BookFormatId? BookFormatId { get; } = default!;
    public ProgrammingLanguageId? ProgrammingLanguageId { get; }
    public string? Isbn { get; }
    public int PublicationYear { get; set; }
    public string? Genre { get; }
    public string? Website { get; set; }
    public string? Notes { get; }
    public DateTime DateCreated { get; }
}
