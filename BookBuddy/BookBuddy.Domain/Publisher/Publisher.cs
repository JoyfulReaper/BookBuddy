using BookBuddy.Domain.Common;
using BookBuddy.Domain.Publiser;

namespace BookBuddy.Domain.Publisher;

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
