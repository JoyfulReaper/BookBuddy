using BookBuddy.Domain.BookAggregate.Entities;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookFormatRepository
{
    Task<BookFormat> GetBookFormatAsync(int id);
    Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync();
    Task<BookFormat> AddBookFormatAsync(BookFormat author, IDbTransaction? transaction);
    Task<BookFormat> UpdateBookFormatAsync(BookFormat author, IDbTransaction? transaction);
    Task DeleteBookFormatAsync(int id);
}
