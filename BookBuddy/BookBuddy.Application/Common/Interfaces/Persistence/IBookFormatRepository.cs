using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookFormatRepository
{
    Task<BookFormat?> GetBookFormatAsync(BookFormatId id,
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);

    Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync(IDbConnection? connection = null, 
        IDbTransaction? transaction = null);

    Task<BookFormatId> AddBookFormatAsync(BookFormat bookFormat,
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);

    Task UpdateBookFormatAsync(BookFormat bookFormat,
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);

    Task<bool> DeleteBookFormatAsync(BookFormatId id, 
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);
    Task<BookFormatId?> BookFormatExists(string format, IDbConnection? connection = null, IDbTransaction? transaction = null);
}
