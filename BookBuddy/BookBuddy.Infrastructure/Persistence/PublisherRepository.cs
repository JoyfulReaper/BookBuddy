using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Infrastructure.Persistence;

internal class PublisherRepository : IPublisherRepository
{
    public Task<PublisherId> AddPublisherAsync(Publisher author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
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
