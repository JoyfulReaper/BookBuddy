using BookBuddy.Domain.BookAggregate.Entities;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IPublisherRepository
{
    Task<Publisher> GetPublisherAsync(int id);
    Task<IEnumerable<Publisher>> GetAllPublishersAsync();
    Task<Publisher> AddPublisherAsync(Publisher author, IDbTransaction? transaction);
    Task<Publisher> UpdatePublisherAsync(Publisher author, IDbTransaction? transaction);
    Task DeletePublisherAsync(int id);
}
