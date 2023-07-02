using BookBuddy.Application.Common.Exceptions;
using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;

namespace BookBuddy.Infrastructure.Persistence;

internal class AuthorRepository : IAuthorRepository, IDisposable
{
    private readonly IDbConnection _connection;

    public AuthorRepository(IDbConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();
    }

    public async Task<AuthorId?> AuthorExists(string? firstName,
        string lastName,
        IDbConnection? connection,
        IDbTransaction? transaction)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"SELECT AuthorId
                       FROM [dbo].[Authors]
                      WHERE [FirstName] = @FirstName
                        AND [LastName] = @LastName;";

        var authorid = await dbConnection.QueryFirstOrDefaultAsync<int?>(sql,
            new
            {
                FirstName = firstName ?? string.Empty,
                LastName = lastName ?? string.Empty
            }, transaction);

        if(authorid == null)
        {
            return null;
        }

        return AuthorId.Create(authorid.Value);
    }

    public async Task<AuthorId> AddAuthorAsync(Author author,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        if( (await AuthorExists(author.FirstName, author.LastName, dbConnection, transaction)) is not null)
        {
            throw new AuthorExistsException($"Author already exists: {author.LastName}, {author.FirstName}");
        }

        var sql = @"INSERT INTO [dbo].[Authors]
                              ([FirstName],
                               [LastName])
                        VALUES
                                (@FirstName, 
                                @LastName);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

        var authorId = await dbConnection.ExecuteScalarAsync<int>(sql,
            new
            {
                author.FirstName,
                author.LastName
            }, transaction);

        return AuthorId.Create(authorId);
    }

    public async Task<bool> DeleteAuthorAsync(AuthorId id,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var checkIfAuthorHasBooksSql = @"SELECT COUNT(*)
                                           FROM [dbo].[Books]
                                          WHERE [AuthorId] = @AuthorId;";

        var hasBooks = await dbConnection.ExecuteScalarAsync<int>(checkIfAuthorHasBooksSql, new { id }, transaction);
        if (hasBooks > 0)
        {
            throw new Exception("Cannot delete author that has books.");
        }

        var sql = @"DELETE FROM [dbo].[Authors]
                     WHERE [Id] = @Id";

        return (await _connection.ExecuteAsync(sql, new { id }, transaction)) > 0;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"SELECT [AuthorId],
                           [FirstName],
                           [LastName]
                      FROM [dbo].[Authors];";

        var author = await dbConnection.QueryAsync<AuthorDto>(sql, null, transaction);
        var output = new List<Author>();
        foreach (var item in author)
        {
            var authorToOutput = AuthorDto.ToAuthor(item);
            if (authorToOutput is not null)
            {
                output.Add(authorToOutput);
            }
        }

        return output;
    }

    public async Task<Author?> GetAuthorAsync(AuthorId id,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"SELECT [AuthorId],
                           [FirstName],
                           [LastName]
                      FROM [dbo].[Authors]
                     WHERE [AuthorId] = @AuthorId";

        var author = await dbConnection.QuerySingleOrDefaultAsync<AuthorDto>(sql, new { AuthorId = id.Value }, transaction);
        return AuthorDto.ToAuthor(author);
    }

    public Task UpdateAuthorAsync(Author author,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"UPDATE [dbo].[Authors]
                       SET [FirstName] = @FirstName,
                           [LastName] = @LastName
                     WHERE [Id] = @Id";

        return dbConnection.ExecuteAsync(sql, new
        {
            author.FirstName,
            author.LastName,
            AuthorId = author.Id.Value
        }, transaction);
    }
}

internal class AuthorDto
{
    int AuthorId { get; set; }
    string FirstName { get; set; } = default!;
    string LastName { get; set; } = default!;

    public static Author? ToAuthor(AuthorDto? dto)
    {
        if (dto is null)
        {
            return null;
        }

        return Author.Create(Domain.BookAggregate.ValueObjects.AuthorId.Create(dto.AuthorId),
            dto.FirstName,
            dto.LastName);
    }
}