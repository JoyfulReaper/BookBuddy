using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IAuthorRepository
{
    Task<Author> GetAuthorAsync(AuthorId id);
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<AuthorId> AddAuthorAsync(Author author, IDbTransaction? transaction);
    Task UpdateAuthorAsync(Author author, IDbTransaction? transaction);
    Task DeleteAuthorAsync(AuthorId id);
}
