using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;

namespace BookBuddy.Infrastructure.Persistence;

internal class PublisherRepository : IPublisherRepository, IDisposable
{
    private readonly IDbConnection _connection;

    public PublisherRepository(ISqlConnectionFactory sqlConnectionFactory, IDbTransaction? transaction)
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

    public async Task<bool> DeletePublisherAsync(int id, IDbTransaction? transaction)
    {
        var checkIfPublisherHasBooksSql = @"SELECT COUNT(*)
                                            FROM [dbo].[Books]
                                           WHERE [PublisherId] = @PublisherId;";

        var hasBooks = await _connection.ExecuteScalarAsync<int>(checkIfPublisherHasBooksSql, new { id }, transaction);
        if (hasBooks > 0)
        {
            throw new Exception("Cannot delete publisher that has books.");
        }

        var sql = @"DELETE FROM [dbo].[Publishers]
                      WHERE [Id] = @Id";

        return (await _connection.ExecuteAsync(sql, new { id }, transaction)) > 0;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync(IDbTransaction? transaction)
    {
        var sql = @"SELECT [PublisherId],
                           [Name],
                           [Website],
                           [DateCreated]
                      FROM [dbo].[Publishers]
                     WHERE [Id] = @Id";

        var publishers = await _connection.QueryAsync<PublisherDto>(sql, null, transaction);
        var output = new List<Publisher>();
        foreach (var publisher in publishers)
        {
            var publisherToAdd = PublisherDto.ToPublisher(publisher);
            if(publisherToAdd is not null)
                output.Add(publisherToAdd);
        }

        return output;
    }

    public async Task<Publisher> GetPublisherAsync(PublisherId publisherId, IDbTransaction? transaction)
    {
        var sql = @"SELECT [PublisherId],
                           [Name],
                           [Website],
                           [DateCreated]
                      FROM [dbo].[Publishers]
                     WHERE [Id] = @Id";

        var publisher = await _connection.QuerySingleOrDefaultAsync<Publisher>(sql, new { Id = publisherId.Value }, transaction);
        return publisher;
    }

    public async Task UpdatePublisherAsync(Publisher publisher, IDbTransaction? transaction)
    {
        var sql = @"UPDATE [dbo].[Publishers]
                       SET [Name] = @Name,
                           [Website] = @Website
                     WHERE [Id] = @Id";

        await _connection.ExecuteAsync(sql, new
        {
            Name = publisher.Name,
            Website = publisher.Website,
        }, transaction);
    }
}

internal class PublisherDto
{
    public int PublisherId { get; set; }
    public string Name { get; set; } = default!;
    public string Website { get; set; } = default!;
    public DateTime DateCreated { get; set; }

    public static Publisher? ToPublisher(PublisherDto? dto)
    {
        return Publisher.Create(Domain.BookAggregate.ValueObjects.PublisherId.Create(dto.PublisherId),
            dto.Name,
            dto.Website, 
            dto.DateCreated);
    }
}