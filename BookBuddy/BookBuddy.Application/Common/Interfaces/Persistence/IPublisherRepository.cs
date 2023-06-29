using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IPublisherRepository
{
    Task<Publisher> GetPublisherAsync(int id);
    Task<IEnumerable<Publisher>> GetAllPublishersAsync();
    Task<PublisherId> AddPublisherAsync(Publisher author, IDbTransaction? transaction);
    Task UpdatePublisherAsync(Publisher author, IDbTransaction? transaction);
    Task DeletePublisherAsync(int id);
}
