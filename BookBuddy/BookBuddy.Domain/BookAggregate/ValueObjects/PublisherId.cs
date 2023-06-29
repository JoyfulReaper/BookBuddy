using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate.ValueObjects;

public class PublisherId : ValueObject
{
    public int Value { get; }

    private PublisherId(int publisherId)
    {
        Value = publisherId;
    }

    public static PublisherId Create(int publisherId)
    {
        return new PublisherId(publisherId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
