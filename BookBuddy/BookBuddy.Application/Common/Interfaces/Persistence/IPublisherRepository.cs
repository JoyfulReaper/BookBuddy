using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IPublisherRepository
{
    Task<Publisher?> GetPublisherAsync(PublisherId publisherId,
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);
    Task<IEnumerable<Publisher>> GetAllPublishersAsync(IDbConnection? connection = null, 
        IDbTransaction? transaction = null);

    Task<PublisherId> AddPublisherAsync(Publisher author,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null);

    Task UpdatePublisherAsync(Publisher author,
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);

    Task<bool> DeletePublisherAsync(PublisherId id, 
        IDbConnection? connection = null, 
        IDbTransaction? transaction = null);
    Task<PublisherId?> PublisherExists(string name, string website, IDbConnection? connection, IDbTransaction? transaction);
}
