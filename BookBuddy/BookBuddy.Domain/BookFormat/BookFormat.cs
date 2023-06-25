using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookFormat;

public class BookFormat : Entity<BookFormatId>
{
    public BookFormatId BookFormatId { get; }
    public string Format { get; } = default!;
    public DateTime DateCreated { get; }

    private BookFormat(BookFormatId bookFormatId,
        string format,
        DateTime dateCreated) : base(bookFormatId)
    {
        BookFormatId = bookFormatId;
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