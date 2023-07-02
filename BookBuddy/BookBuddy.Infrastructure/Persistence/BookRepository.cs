using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate;
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

    public async Task<BookId> AddBookAsync(Book book, IDbTransaction? transaction)
    {
        var dbTransaction = transaction ?? _dbConnection.BeginTransaction();
        try
        {
            var booksql = @"INSERT INTO [dbo].[Books]
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

            var bookId = await _dbConnection.ExecuteScalarAsync<int>(booksql, 
                new {
                    book.Title,
                    AuthorId = book.Author?.Id,
                    PublisherId = book.Publisher?.Id,
                    BookFormatId = book.BookFormat?.Id,
                    ProgrammingLanguageId = book.ProgrammingLanguage?.Id,
                    book.Isbn,
                    book.PublicationYear,
                    book.Genre,
                    book.Website,
                    book.Notes
                },
                dbTransaction);

            if (book.Author is not null)
                await _authorRepository.AddAuthorAsync(book.Author, transaction);
            if(book.Publisher is not null)
                await _publisherRepository.AddPublisherAsync(book.Publisher, transaction);
            if(book.BookFormat is not null)
                await _bookFormatRepository.AddBookFormatAsync(book.BookFormat, transaction);
            if(book.ProgrammingLanguage is not null)
                await _programmingLanguageRepository.AddProgrammingLanguageAsync(book.ProgrammingLanguage, transaction);

            transaction.Commit();
            return BookId.Create(bookId);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> DeleteBookAsync(BookId id, IDbTransaction? transaction)
    {
        var sql = @"UPDATE [dbo].[Books]
                        SET [DateDeleted] = SYSUTCDATETIME()
                        WHERE BookId = @BookId;";

         return await _dbConnection.ExecuteAsync(sql, new { BookId = id.Value }, transaction) > 0;
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(IDbTransaction? transaction)
    {
        var sql = @"SELECT BookId, Title, AuthorId, PublisherId, BookformatId, ProgrammingLanguageId, ISBN, PublicationYear, Genre, Website, Notes, DateCreated
                          FROM [dbo].[Books] WHERE DateDeleted IS NULL;";

        return await _dbConnection.QueryAsync<Book>(sql, transaction);
    }

    public async Task<Book?> GetBookAsync(BookId id, IDbTransaction? transaction)
    {
        var sql = @"SELECT BookId, Title, AuthorId, PublisherId, BookformatId, ProgrammingLanguageId, ISBN, PublicationYear, Genre, Website, Notes, DateCreated
                          FROM [dbo].[Books] WHERE DateDeleted IS NULL AND BookId = @BookId;";

        return await _dbConnection.QuerySingleOrDefaultAsync<Book>(sql, new { id = id.Value }, transaction);
    }

    public Task UpdateBookAsync(Book book, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}
