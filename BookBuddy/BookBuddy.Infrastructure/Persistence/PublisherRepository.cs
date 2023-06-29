using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;

namespace BookBuddy.Infrastructure.Persistence;

internal class PublisherRepository : IPublisherRepository
{
    private readonly IDbConnection _connection;

    public PublisherRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();
    }

    public async Task<PublisherId> AddPublisherAsync(Publisher author, IDbTransaction? transaction)
    {
        var sql = @"INSERT INTO [dbo].[Publishers]
                            ([Name], [Website])
                          VALUES
                            (@Name, @Website)
                          SELECT CAST(SCOPE_IDENTITY() AS INT)";

        var publisherId = await _connection.ExecuteScalarAsync<int>(sql,
            new
            {
                author.Name,
                author.Website
            }, transaction);

        return PublisherId.Create(publisherId);
    }

    public Task DeletePublisherAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Publisher>> GetAllPublishersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Publisher> GetPublisherAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePublisherAsync(Publisher author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}
