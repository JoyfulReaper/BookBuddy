using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate;
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

    public async Task<BookId> AddBookAsync(Book book, IDbTransaction? transaction)
    {
        _dbConnection.Open();
        var dbTransaction = transaction ?? _dbConnection.BeginTransaction();
        try
        {
            int? authorId = null;
            int? publisherId = null;
            int? bookFormatId = null;
            int? programmingLanguageId = null;
            if (book.Author is not null)
                authorId = (await _authorRepository.AddAuthorAsync(book.Author, dbTransaction)).Value;
            if (book.Publisher is not null)
                publisherId = (await _publisherRepository.AddPublisherAsync(book.Publisher, dbTransaction)).Value;
            if (book.BookFormat is not null)
                bookFormatId = (await _bookFormatRepository.AddBookFormatAsync(book.BookFormat, dbTransaction)).Value;
            if (book.ProgrammingLanguage is not null)
                programmingLanguageId = authorId = (await _programmingLanguageRepository.AddProgrammingLanguageAsync(book.ProgrammingLanguage, dbTransaction)).Value;


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
                                    ,@Genre
                                    ,@Website
                                    ,@Notes)
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            var bookId = await _dbConnection.ExecuteScalarAsync<int>(booksql, 
                new {
                    book.Title,
                    AuthorId = authorId,
                    PublisherId = publisherId,
                    BookFormatId = bookFormatId,
                    ProgrammingLanguageId = programmingLanguageId,
                    book.Isbn,
                    book.PublicationYear,
                    book.Genre,
                    book.Website,
                    book.Notes
                },
                dbTransaction);

            dbTransaction.Commit();
            return BookId.Create(bookId);
        }
        catch
        {
            dbTransaction.Rollback();
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

        var books = await _dbConnection.QueryAsync<BookDto>(sql, transaction);

        throw new NotImplementedException();
    }

    public async Task<Book?> GetBookAsync(BookId id, IDbTransaction? transaction)
    {
        var transactionToUse = transaction ?? _dbConnection.BeginTransaction();

        var sql = @"SELECT BookId, Title, AuthorId, PublisherId, BookformatId, ProgrammingLanguageId, ISBN, PublicationYear, Genre, Website, Notes, DateCreated
                          FROM [dbo].[Books] WHERE DateDeleted IS NULL AND BookId = @BookId;";

        var book = await _dbConnection.QuerySingleOrDefaultAsync<BookDto>(sql, new { id = id.Value }, transactionToUse);

        Author? author = null;
        if( book.AuthorId is not null)
        {
             author = await _authorRepository.GetAuthorAsync(AuthorId.Create(book.AuthorId.Value), transactionToUse);
        }

        Publisher? publisher = null;
        if(book.PublisherId is not null)
        {
            publisher = await _publisherRepository.GetPublisherAsync(PublisherId.Create(book.PublisherId.Value), transactionToUse);
        }

        BookFormat? bookFormat = null;
        if(book.BookFormatId is not null)
        {
            bookFormat = await _bookFormatRepository.GetBookFormatAsync(BookFormatId.Create(book.BookFormatId.Value), transactionToUse);
        }

        ProgrammingLanguage? programmingLanguage = null;
        if(book.ProgrammingLanguageId is not null)
        {
            programmingLanguage = await _programmingLanguageRepository.GetProgrammingLanguageAsync(ProgrammingLanguageId.Create(book.ProgrammingLanguageId.Value), transactionToUse);
        }

        return BookDto.ToBook(book, author, publisher, bookFormat, programmingLanguage);
    }

    public Task UpdateBookAsync(Book book, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}

internal class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int? AuthorId { get; set; }
    public int? PublisherId { get; set; }
    public int? BookFormatId { get; set; }
    public int? ProgrammingLanguageId { get; set; }
    public string? ISBN { get; set; }
    public int PublicationYear { get; set; }
    public string? Genre { get; set; }
    public string? Website { get; set; }
    public string? Notes { get; set; }
    public DateTime DateCreated { get; set; }

    public static Book? ToBook(BookDto? dto, Author? author, Publisher? publisher, BookFormat? bookFormat, ProgrammingLanguage? programmingLanguage)
    {
        if (dto is null)
            return null;

        return Book.Create(Domain.BookAggregate.ValueObjects.BookId.Create(dto.BookId),
            dto.Title,
            author,
            publisher,
            bookFormat,
            programmingLanguage,
            dto.ISBN,
            dto.PublicationYear,
            dto.Genre,
            dto.Website,
            dto.Notes,
            dto.DateCreated);
    }
}