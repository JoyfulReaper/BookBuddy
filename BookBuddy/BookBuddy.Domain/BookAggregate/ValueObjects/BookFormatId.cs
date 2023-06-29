using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate.ValueObjects;
public class BookFormatId : ValueObject
{
    public int Value { get; }

    private BookFormatId(int bookId)
    {
        Value = bookId;
    }

    public static BookFormatId Create(int bookId)
    {
        return new BookFormatId(bookId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
