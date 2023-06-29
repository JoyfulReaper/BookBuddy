using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate.Entities;

public class Publisher : Entity<PublisherId>
{
    public PublisherId PublisherId { get; }
    public string Name { get; } = default!;
    public string? Website { get; } = default!;
    public DateTime DateCreated { get; }

    private Publisher(PublisherId publisherId,
        string name,
        string? website,
        DateTime dateCreated) : base(publisherId)
    {
        PublisherId = publisherId;
        Name = name;
        Website = website;
        DateCreated = dateCreated;
    }

    public static Publisher Create(PublisherId publisherId,
        string name,
        string? webstite,
        DateTime dateCreated)
    {
        return new Publisher(publisherId,
            name,
            webstite,
            dateCreated);
    }
}
