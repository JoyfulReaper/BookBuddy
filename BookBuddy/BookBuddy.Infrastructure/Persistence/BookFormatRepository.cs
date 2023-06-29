using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;


namespace BookBuddy.Infrastructure.Persistence;
internal class BookFormatRepository : IBookFormatRepository
{
    public Task<BookFormatId> AddBookFormatAsync(BookFormat author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookFormatAsync(BookFormatId id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BookFormat> GetBookFormatAsync(BookFormatId id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBookFormatAsync(BookFormat author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}
