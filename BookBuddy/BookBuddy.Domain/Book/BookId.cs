using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.Book;
public class BookId : ValueObject
{
    public int Value { get; }

    private BookId(int bookId)
    {
        Value = bookId;
    }

    public static BookId Create(int bookId)
    {
        return new BookId(bookId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
