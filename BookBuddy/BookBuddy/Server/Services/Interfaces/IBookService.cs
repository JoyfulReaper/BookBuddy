using BookBuddy.Server.Data;
using BookBuddy.Shared.Contracts;

namespace BookBuddy.Server.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
    Task<List<Book>> SearchBooksAsync(BookSearchOptions bookSearchoptions);
}