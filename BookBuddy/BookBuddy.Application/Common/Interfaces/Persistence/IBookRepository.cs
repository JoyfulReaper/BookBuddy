using BookBuddy.Domain.BookAggregate.Book;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookRepository
{
    Task<Book> GetBookAsync(int id);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> AddBookAsync(Book author);
    Task<Book> UpdateBookAsync(Book author);
    Task DeleteBookAsync(int id); 
}
