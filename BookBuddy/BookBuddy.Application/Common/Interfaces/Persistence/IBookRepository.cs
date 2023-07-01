using BookBuddy.Domain.BookAggregate;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookRepository
{
    Task<Book?> GetBookAsync(BookId id);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<BookId> AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(BookId id); 
}
