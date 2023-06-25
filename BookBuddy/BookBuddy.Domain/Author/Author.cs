using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.Author;

public class Author : Entity<AuthorId>
{
    private Author(AuthorId authorId,
        string? firstName,
        string lastName) : base(authorId)
    {
        AuthorId = authorId;
        FirstName = firstName;
        LastName = lastName;
    }

    public AuthorId AuthorId { get; }
    public string? FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public DateTime DateCreated { get; set; }

    public static Author Create(AuthorId authorId,
        string? firstName,
        string lastName)
    {
        return new Author(authorId,
            firstName, 
            lastName);
    }
}
