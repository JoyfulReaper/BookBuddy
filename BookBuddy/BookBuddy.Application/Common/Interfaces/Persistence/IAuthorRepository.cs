using BookBuddy.Domain.BookAggregate.Entities;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IAuthorRepository
{
    Task<Author> GetAuthorAsync(int id);
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author> AddAuthorAsync(Author author, IDbTransaction? transaction);
    Task<Author> UpdateAuthorAsync(Author author, IDbTransaction? transaction);
    Task DeleteAuthorAsync(int id);
}
