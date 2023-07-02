using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookFormatRepository
{
    Task<BookFormat> GetBookFormatAsync(BookFormatId id, IDbTransaction? transaction = null);
    Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync(IDbTransaction? transaction = null);
    Task<BookFormatId> AddBookFormatAsync(BookFormat bookFormat, IDbTransaction? transaction = null);
    Task UpdateBookFormatAsync(BookFormat bookFormat, IDbTransaction? transaction = null);
    Task DeleteBookFormatAsync(BookFormatId id, IDbTransaction? transaction = null);
}
