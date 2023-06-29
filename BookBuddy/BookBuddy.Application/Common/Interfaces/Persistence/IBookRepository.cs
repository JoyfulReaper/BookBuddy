using BookBuddy.Domain.BookAggregate.Book;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookRepository
{
    Task<Book> GetBookAsync(BookId id);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<BookId> AddBookAsync(Book author);
    Task UpdateBookAsync(Book author);
    Task DeleteBookAsync(BookId id); 
}
