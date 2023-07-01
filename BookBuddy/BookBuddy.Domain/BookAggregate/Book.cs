using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate;

public class Book : Entity<BookId>
{
    private Book(BookId bookId,
        string title,
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
        BookId = bookId;
        Title = title;
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

    public BookId BookId { get; }
    public string Title { get; } = default!;
    public string? Isbn { get; }
    public int PublicationYear { get; set; }
    public string? Genre { get; }
    public string? Website { get; set; }
    public string? Notes { get; }
    public DateTime DateCreated { get; }

    public Author? Author { get; }
    public BookFormat? BookFormat { get; }
    public Publisher? Publisher { get; }
    public ProgrammingLanguage? ProgrammingLanguage { get; }
}
