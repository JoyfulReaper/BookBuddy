using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;

namespace BookBuddy.Infrastructure.Persistence;

internal class AuthorRepository : IAuthorRepository
{
    private readonly IDbConnection _connection;

    public AuthorRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();
    }

    public async Task<AuthorId> AddAuthorAsync(Author author, IDbTransaction? transaction)
    {
        var sql = @"INSERT INTO [dbo].[Authors]
                              ([FirstName],
                               [LastName])
                        VALUES
                                (@FirstName, 
                                @LastName);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

        var authorId = await _connection.ExecuteScalarAsync<int>(sql, 
            new {
                author.FirstName,
                author.LastName
            }, transaction);

        return AuthorId.Create(authorId);
    }

    public Task DeleteAuthorAsync(AuthorId id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Author> GetAuthorAsync(AuthorId id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAuthorAsync(Author author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}
