using BookBuddy.Server.Data;
using BookBuddy.Server.Services.Interfaces;
using BookBuddy.Shared.Contracts;
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

    public async Task<List<Book>> SearchBooksAsync(BookSearchOptions bookSearchoptions)
    {
        var books = await _context.Books
            .Where(b => !b.IsDeleted &&
            (bookSearchoptions.Title == null || b.Title.Contains(bookSearchoptions.Title)) &&
            (bookSearchoptions.Author == null || b.Author.Contains(bookSearchoptions.Author)) &&
            (bookSearchoptions.Publisher == null || b.Publisher != null && b.Publisher.Contains(bookSearchoptions.Publisher)) &&
            (bookSearchoptions.ISBN == null || b.ISBN != null && b.ISBN.Contains(bookSearchoptions.ISBN)) &&
            (bookSearchoptions.Genre == null || b.Genre != null && b.Genre.Contains(bookSearchoptions.Genre)) &&
            (!bookSearchoptions.PublicationYear.HasValue || b.PublicationYear == bookSearchoptions.PublicationYear) &&
            (!bookSearchoptions.Format.HasValue || b.Format == bookSearchoptions.Format))
            .ToListAsync();

        return books;
    }
}