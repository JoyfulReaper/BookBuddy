using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate;

public class Book : Entity<BookId>
{
    private Book(BookId bookId,
        string title,
        AuthorId? authorId,
        PublisherId? publisherId,
        BookFormatId? bookFormatId,
        ProgrammingLanguageId? programmingLanguageId,
        Author? author,
        Publisher? publisher,
        BookFormat? bookFormat,
        ProgrammingLanguage? programmingLanguage,
        string? isbn,
        int publicationYear,
        string? genre,
        string? website,
        string? notes,
        DateTime datecreated) : base (bookId)
    {
        Id = bookId;
        Title = title;
        AuthorId = authorId;
        PublisherId = publisherId;
        BookFormatId = bookFormatId;
        ProgrammingLanguageId = programmingLanguageId;
        Author = author;
        Publisher = publisher;
        BookFormat = bookFormat;
        ProgrammingLanguage = programmingLanguage;
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
        Author? author,
        Publisher? publisher,
        BookFormat? bookFormat,
        ProgrammingLanguage? programmingLanguage,
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
            author,
            publisher,
            bookFormat,
            programmingLanguage,
            isbn,
            publicationYear,
            genre,
            website,
            notes,
            dateCreated);
    }

    public AuthorId? AuthorId { get; }
    public PublisherId? PublisherId { get; }
    public BookFormatId? BookFormatId { get; }
    public ProgrammingLanguageId? ProgrammingLanguageId { get; }
    public string Title { get; } = default!;
    public string? Isbn { get; }
    public int PublicationYear { get; }
    public string? Genre { get; }
    public string? Website { get; }
    public string? Notes { get; }
    public DateTime DateCreated { get; }

    public Author? Author { get; }
    public BookFormat? BookFormat { get; }
    public Publisher? Publisher { get; }
    public ProgrammingLanguage? ProgrammingLanguage { get; }
}
