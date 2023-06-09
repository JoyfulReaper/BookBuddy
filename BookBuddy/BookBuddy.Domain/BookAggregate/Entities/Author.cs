﻿using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate.Entities;

public class Author : Entity<AuthorId>
{
    private Author(AuthorId authorId,
        string? firstName,
        string lastName) : base(authorId)
    {
        Id = authorId;
        FirstName = firstName;
        LastName = lastName;
    }

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
