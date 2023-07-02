using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IAuthorRepository
{
    Task<Author?> GetAuthorAsync(AuthorId id, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null);

    Task<IEnumerable<Author>> GetAllAuthorsAsync(IDbConnection? connection = null,
        IDbTransaction ? transaction = null);

    Task<AuthorId> AddAuthorAsync(Author author, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null);

    Task UpdateAuthorAsync(Author author,
        IDbConnection? connection = null,
        IDbTransaction ? transaction = null);

    Task<bool> DeleteAuthorAsync(AuthorId id,
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);
}
