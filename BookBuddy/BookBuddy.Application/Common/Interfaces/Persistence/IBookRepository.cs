using BookBuddy.Domain.BookAggregate;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookRepository
{
    Task<Book?> GetBookAsync(BookId id, IDbTransaction? transaction = null);
    Task<IEnumerable<Book>> GetAllBooksAsync(IDbTransaction? transaction = null);
    Task<BookId> AddBookAsync(Book book, IDbTransaction? transaction = null);
    Task UpdateBookAsync(Book book, IDbTransaction? transaction = null);
    Task<bool> DeleteBookAsync(BookId id, IDbTransaction? transaction = null); 
}
