using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.Author;

public class AuthorId : ValueObject
{
    public int Value { get; }

    private AuthorId(int authorId)
    {
        Value = authorId;
    }

    public static AuthorId Create(int authorId)
    {
        return new AuthorId(authorId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
