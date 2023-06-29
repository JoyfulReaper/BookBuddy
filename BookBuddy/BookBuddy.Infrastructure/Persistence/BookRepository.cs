using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Book;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;

namespace BookBuddy.Infrastructure.Persistence;
internal class BookRepository : IBookRepository, IDisposable
{
    private readonly IDbConnection _dbConnection;
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookFormatRepository _bookFormatRepository;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
    private readonly IPublisherRepository _publisherRepository;

    public BookRepository(ISqlConnectionFactory sqlConnectionFactory,
        IAuthorRepository authorRepository,
        IBookFormatRepository bookFormatRepository,
        IProgrammingLanguageRepository programmingLanguageRepository,
        IPublisherRepository publisherRepository)
    {
        _dbConnection = sqlConnectionFactory.CreateConnection();
        _authorRepository = authorRepository;
        _bookFormatRepository = bookFormatRepository;
        _programmingLanguageRepository = programmingLanguageRepository;
        _publisherRepository = publisherRepository;
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        var transaction = _dbConnection.BeginTransaction();
        try
        {
            var booksql = @"INSERT INTO [dbo].[Book]
                                    ([Title]
                                    ,[AuthorId]
                                    ,[PublisherId]
                                    ,[BookFormatId]
                                    ,[ProgrammingLanguageId]
                                    ,[ISBN]
                                    ,[PublicationYear]
                                    ,[Genre]
                                    ,[Website]
                                    ,[Notes])
                                VALUES
                                    (@Title
                                    ,@AuthorId
                                    ,@PublisherId
                                    ,@BookFormatId  
                                    ,@ProgrammingLanguageId
                                    ,@Isbn
                                    ,@PublicationYear
                                    ,@Genere
                                    ,@Website
                                    ,@Notes)
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            var bookId = await _dbConnection.ExecuteScalarAsync<int>(booksql, book, transaction);

            if (book.Author is not null)
                 await _authorRepository.AddAuthorAsync(book.Author, transaction);
            if(book.Publisher is not null)
                await _publisherRepository.AddPublisherAsync(book.Publisher, transaction);
            if(book.BookFormat is not null)
                await _bookFormatRepository.AddBookFormatAsync(book.BookFormat, transaction);
            if(book.ProgrammingLanguage is not null)
                await _programmingLanguageRepository.AddProgrammingLanguageAsync(book.ProgrammingLanguage, transaction);

            transaction.Commit();

            return await GetBookAsync(bookId);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public Task DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }
}
