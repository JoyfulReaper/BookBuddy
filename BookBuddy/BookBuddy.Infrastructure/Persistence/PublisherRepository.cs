using BookBuddy.Application.Common.Exceptions;
using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;
using System.Data.Common;

namespace BookBuddy.Infrastructure.Persistence;

internal class PublisherRepository : IPublisherRepository, IDisposable
{
    private readonly IDbConnection _connection;

    public PublisherRepository(IDbConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();
    }

    public async Task<PublisherId?> PublisherExists(string name,
        string? website, 
        IDbConnection? connection,
        IDbTransaction? transaction)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"SELECT PublisherId
                       FROM [dbo].[Publishers]
                      WHERE [Name] = @Name
                        AND [Website] = @Website;";

        var publisherId = await dbConnection.QueryFirstOrDefaultAsync<int?>(sql,
            new
            {
                Name = name ?? string.Empty,
                Website = website ?? string.Empty
            }, transaction);

        if (publisherId is null)
        {
            return null;
        }

        return PublisherId.Create(publisherId.Value);
    }

    public async Task<PublisherId> AddPublisherAsync(Publisher publisher,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        if ( (await PublisherExists(publisher.Name, publisher.Website, dbConnection, transaction)) is not null )
        {
            throw new PublisherExistsException($"Publisher already exists: {publisher.Name}, {publisher.Website}");
        }

        var sql = @"INSERT INTO [dbo].[Publishers]
                            ([Name], [Website])
                          VALUES
                            (@Name, @Website)
                          SELECT CAST(SCOPE_IDENTITY() AS INT)";

        var publisherId = await dbConnection.ExecuteScalarAsync<int>(sql,
            new
            {
                publisher.Name,
                publisher.Website
            }, transaction);

        return PublisherId.Create(publisherId);
    }

    public async Task<bool> DeletePublisherAsync(PublisherId id,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
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

    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync(
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"SELECT [PublisherId],
                           [Name],
                           [Website],
                           [DateCreated]
                      FROM [dbo].[Publishers]
                     WHERE [Id] = @Id";

        var publishers = await dbConnection.QueryAsync<PublisherDto>(sql, null, transaction);
        var output = new List<Publisher>();
        foreach (var publisher in publishers)
        {
            var publisherToAdd = PublisherDto.ToPublisher(publisher);
            if(publisherToAdd is not null)
                output.Add(publisherToAdd);
        }

        return output;
    }

    public async Task<Publisher?> GetPublisherAsync(PublisherId publisherId,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"SELECT [PublisherId],
                           [Name],
                           [Website],
                           [DateCreated]
                      FROM [dbo].[Publishers]
                     WHERE [PublisherId] = @PublisherId";

        var publisher = await dbConnection.QuerySingleOrDefaultAsync<PublisherDto>(sql, new { PublisherId = publisherId.Value }, transaction);
        return PublisherDto.ToPublisher(publisher);
    }

    public async Task UpdatePublisherAsync(Publisher publisher, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"UPDATE [dbo].[Publishers]
                       SET [Name] = @Name,
                           [Website] = @Website
                     WHERE [Id] = @Id";

        await dbConnection.ExecuteAsync(sql, new
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