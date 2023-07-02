using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate.Entities;

public class BookFormat : Entity<BookFormatId>
{
    public string Format { get; } = default!;
    public DateTime DateCreated { get; }

    private BookFormat(BookFormatId bookFormatId,
        string format,
        DateTime dateCreated) : base(bookFormatId)
    {
        Id = bookFormatId;
        Format = format;
        DateCreated = dateCreated;
    }

    public static BookFormat Create(BookFormatId bookFormatId,
        string format,
        DateTime dateCreated)
    {
        return new BookFormat(bookFormatId,
            format,
            dateCreated);
    }
}