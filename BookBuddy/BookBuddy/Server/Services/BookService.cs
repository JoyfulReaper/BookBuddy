using BookBuddy.Server.Data;
using BookBuddy.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookBuddy.Server.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        var result = await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book != null)
        {
            book.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        return await _context.Books.Where(b => !b.IsDeleted).ToListAsync();
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return book;
    }
}
