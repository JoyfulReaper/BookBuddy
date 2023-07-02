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

    public AuthorRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();
    }

    public async Task<AuthorId> AddAuthorAsync(Author author, IDbTransaction? transaction = null)
    {
        var sql = @"INSERT INTO [dbo].[Authors]
                              ([FirstName],
                               [LastName])
                        VALUES
                                (@FirstName, 
                                @LastName);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

        var authorId = await _connection.ExecuteScalarAsync<int>(sql,
            new
            {
                author.FirstName,
                author.LastName
            }, transaction);

        return AuthorId.Create(authorId);
    }

    public async Task<bool> DeleteAuthorAsync(AuthorId id, IDbTransaction? transaction = null)
    {
        var checkIfAuthorHasBooksSql = @"SELECT COUNT(*)
                                           FROM [dbo].[Books]
                                          WHERE [AuthorId] = @AuthorId;";

        var hasBooks = await _connection.ExecuteScalarAsync<int>(checkIfAuthorHasBooksSql, new { id }, transaction);
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

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(IDbTransaction? transaction = null)
    {
        var sql = @"SELECT [AuthorId],
                           [FirstName],
                           [LastName]
                      FROM [dbo].[Authors];";

        var author = await _connection.QueryAsync<AuthorDto>(sql, null, transaction);
        var output = new List<Author>();
        foreach (var item in author)
        {
            output.Add(AuthorDto.ToAuthor(item));
        }

        return output;
    }

    public async Task<Author> GetAuthorAsync(AuthorId id, IDbTransaction? transaction = null)
    {
        var sql = @"SELECT [AuthorId],
                           [FirstName],
                           [LastName]
                      FROM [dbo].[Authors]
                     WHERE [Id] = @Id";

        var author = await _connection.QuerySingleOrDefaultAsync<AuthorDto>(sql, new { id }, transaction);
        return AuthorDto.ToAuthor(author);
    }

    public Task UpdateAuthorAsync(Author author, IDbTransaction? transaction = null)
    {
        var sql = @"UPDATE [dbo].[Authors]
                       SET [FirstName] = @FirstName,
                           [LastName] = @LastName
                     WHERE [Id] = @Id";

        return _connection.ExecuteAsync(sql, new
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

    public static Author ToAuthor(AuthorDto dto)
    {
        return Author.Create(Domain.BookAggregate.ValueObjects.AuthorId.Create(dto.AuthorId),
            dto.FirstName,
            dto.LastName);
    }
}