using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;


namespace BookBuddy.Infrastructure.Persistence;
internal class BookFormatRepository : IBookFormatRepository
{
    private readonly IDbConnection _connection;

    public BookFormatRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();    
    }

    public async Task<BookFormatId> AddBookFormatAsync(BookFormat bookFormat, IDbTransaction? transaction)
    {
        var sql = @"INSERT INTO [dbo].[BookFormats]
                            (Format)
                          VALEUS
                             (@Format)
                          SELECT CAST (SCOPE_IDENTITY() AS INT)";

        var bookFormatId = await _connection.ExecuteScalarAsync<int>(sql,
            new
            {
                bookFormat.BookFormatId
            }, transaction);

        return BookFormatId.Create(bookFormatId);
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

    public Task UpdateBookFormatAsync(BookFormat bookFormat, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}
