using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;


namespace BookBuddy.Infrastructure.Persistence;
internal class BookFormatRepository : IBookFormatRepository, IDisposable
{
    private readonly IDbConnection _connection;

    public BookFormatRepository(IDbConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();    
    }

    public async Task<BookFormatId> AddBookFormatAsync(BookFormat bookFormat,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"INSERT INTO [dbo].[BookFormats]
                            (Format)
                          VALUES
                             (@Format)
                          SELECT CAST (SCOPE_IDENTITY() AS INT)";

        var bookFormatId = await dbConnection.ExecuteScalarAsync<int>(sql,
            new
            {
                bookFormat.Format
            }, transaction);

        return BookFormatId.Create(bookFormatId);
    }

    public async Task<bool> DeleteBookFormatAsync(BookFormatId id, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var bookfomratIsAssigned = @"SELECT COUNT(*)
                                        FROM [dbo].[Books]
                                       WHERE [BookFormatId] = @BookFormatId;";

        var isAssigned = await dbConnection.ExecuteScalarAsync<int>(bookfomratIsAssigned, new { id }, transaction);

        if (isAssigned > 0)
        {
            throw new Exception("Cannot delete book format that is assigned to books.");
        }

        var sql = @"DELETE FROM [dbo].[BookFormats]
                      WHERE [Id] = @Id";

        return (await dbConnection.ExecuteAsync(sql, new { id }, transaction)) > 0;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public async Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync(IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = "SELECT BookFormatId, Format, DateCreated FROM BookFormats WHERE BookFormatId = @BookFormatId";
        var bookFormats = await dbConnection.QueryAsync<BookFormatDto>(sql, null, transaction);

        var output = new List<BookFormat>();
        foreach(var book in bookFormats)
        {
            var formatToAdd = BookFormatDto.ToBookFormat(book);
            if(formatToAdd is not null)
            {
                output.Add(formatToAdd);
            }
        }

        return output;
    }

    public async Task<BookFormat?> GetBookFormatAsync(BookFormatId id,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var sql = "SELECT BookFormatId, Format, DateCreated FROM BookFormats WHERE BookFormatId = @BookFormatId";
        var bookFormat = await dbConnection.QuerySingleOrDefaultAsync<BookFormatDto>(sql, new { BookFormatId = id.Value }, transaction);

        return BookFormatDto.ToBookFormat(bookFormat);
    }

    public Task UpdateBookFormatAsync(BookFormat bookFormat, 
        IDbConnection? connection,
        IDbTransaction? transaction)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"UPDATE [dbo].[BookFormats]
                       SET [Format] = @Format
                     WHERE [BookFormatId] = @BookFormatId";

        return dbConnection.ExecuteAsync(sql, new { bookFormat.Id.Value, bookFormat.Format }, transaction);
    }
}

internal class BookFormatDto
{
    public int BookFormatId { get; set; }
    public string Format { get; set; } = default!;
    public DateTime DateCreated { get; set; }

    public static BookFormat? ToBookFormat(BookFormatDto? dto)
    {
        if (dto == null)
        {
            return null!;
        }

        return BookFormat.Create(Domain.BookAggregate.ValueObjects.BookFormatId.Create(dto.BookFormatId), 
            dto.Format,
            dto.DateCreated);
    }
}