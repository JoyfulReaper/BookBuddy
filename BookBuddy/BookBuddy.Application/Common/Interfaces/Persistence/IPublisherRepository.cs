using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IPublisherRepository
{
    Task<Publisher> GetPublisherAsync(int id, IDbTransaction? transaction = null);
    Task<IEnumerable<Publisher>> GetAllPublishersAsync(IDbTransaction? transaction = null);
    Task<PublisherId> AddPublisherAsync(Publisher author, IDbTransaction? transaction = null);
    Task UpdatePublisherAsync(Publisher author, IDbTransaction? transaction = null);
    Task<bool> DeletePublisherAsync(int id, IDbTransaction? transaction = null);
}
