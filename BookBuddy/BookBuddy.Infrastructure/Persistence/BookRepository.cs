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
    private readonly IDbConnection _connection;
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookFormatRepository _bookFormatRepository;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
    private readonly IPublisherRepository _publisherRepository;

    public BookRepository(IDbConnectionFactory sqlConnectionFactory,
        IAuthorRepository authorRepository,
        IBookFormatRepository bookFormatRepository,
        IProgrammingLanguageRepository programmingLanguageRepository,
        IPublisherRepository publisherRepository)
    {
        _connection = sqlConnectionFactory.CreateConnection();
        _authorRepository = authorRepository;
        _bookFormatRepository = bookFormatRepository;
        _programmingLanguageRepository = programmingLanguageRepository;
        _publisherRepository = publisherRepository;
    }

    public async Task<BookId> AddBookAsync(Book book, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        var dbConnection = connection ?? _connection;
        if(dbConnection.State != ConnectionState.Open)
            dbConnection.Open();

        using var dbTransaction = transaction ?? dbConnection.BeginTransaction();
        try
        {
            int? authorId = book.AuthorId?.Value;
            int? publisherId = book.PublisherId?.Value;
            int? bookFormatId = book.BookFormatId?.Value;
            int? programmingLanguageId = book.ProgrammingLanguageId?.Value;
            if (book.Author is not null)
                authorId = (await _authorRepository.AddAuthorAsync(book.Author, dbConnection, dbTransaction)).Value;
            if (book.Publisher is not null)
                publisherId = (await _publisherRepository.AddPublisherAsync(book.Publisher, dbConnection, dbTransaction)).Value;
            if (book.BookFormat is not null)
                bookFormatId = (await _bookFormatRepository.AddBookFormatAsync(book.BookFormat, dbConnection, dbTransaction)).Value;
            if (book.ProgrammingLanguage is not null)
                programmingLanguageId = authorId = (await _programmingLanguageRepository.AddProgrammingLanguageAsync(book.ProgrammingLanguage, dbConnection, dbTransaction)).Value;


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

            var bookId = await dbConnection.ExecuteScalarAsync<int>(booksql, 
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

    public async Task<bool> BookExists(BookId id, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var sql = "SELECT COUNT(*) FROM Books WHERE BookId = @BookId;";
        var count =  await dbConnection.ExecuteScalarAsync<int>(sql, new { BookId = id.Value }, transaction);
        return count > 0;
    }

    public async Task<bool> DeleteBookAsync(BookId id, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"UPDATE [dbo].[Books]
                        SET [DateDeleted] = SYSUTCDATETIME()
                        WHERE BookId = @BookId;";

         return await dbConnection.ExecuteAsync(sql, new { BookId = id.Value }, transaction) > 0;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"SELECT
	                        b.BookId
                           ,b.Title
                           ,b.AuthorId
                           ,b.PublisherId
                           ,b.BookFormatId
                           ,b.ProgrammingLanguageId
                           ,b.ISBN
                           ,b.PublicationYear
                           ,b.Genre
                           ,b.Website
                           ,b.Notes
                           ,b.DateCreated
                           ,a.*
                           ,p.*
                           ,f.*
                           ,l.*
                        FROM [dbo].[Books] b
                        LEFT JOIN [dbo].[Authors] a
	                        ON b.AuthorId = a.AuthorId
                        LEFT JOIN [dbo].[Publishers] p
	                        ON b.PublisherId = p.PublisherId
                        LEFT JOIN [dbo].[BookFormats] f
	                        ON b.BookFormatId = f.BookFormatId
                        LEFT JOIN [dbo].[ProgrammingLanguages] l
	                        ON b.ProgrammingLanguageId = l.ProgrammingLanguageId
                        WHERE DateDeleted IS NULL;";

        var books = await dbConnection.QueryAsync<BookDto, AuthorDto, PublisherDto, BookFormatDto, ProgrammingLanguageDto, BookDto>(sql, (b, a, p, f, l) => {
            b.Author = a;
            b.Publisher = p;
            b.BookFormat = f;
            b.ProgrammingLanguage = l;
            return b;
        }, splitOn: "AuthorId,PublisherId,BookFormatId,ProgrammingLanguageId");

        var output = new List<Book>();
        foreach (var book in books)
        {
            var bookToAdd = BookDto.ToBook(book,
                book.AuthorId == null ? null : AuthorDto.ToAuthor(book.Author),
                book.PublisherId == null ? null : PublisherDto.ToPublisher(book.Publisher),
                book.BookFormatId == null ? null : BookFormatDto.ToBookFormat(book.BookFormat),
                book.ProgrammingLanguageId == null ? null : ProgrammingLanguageDto.ToProgrammingLanguage(book.ProgrammingLanguage));

            output.Add(bookToAdd);
        }

        return output;
    }

    public async Task<Book?> GetBookAsync(BookId id,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();

        var transactionToUse = transaction ?? dbConnection.BeginTransaction();

        var sql = @"SELECT BookId, Title, AuthorId, PublisherId, BookFormatId, ProgrammingLanguageId, ISBN, PublicationYear, Genre, Website, Notes, DateCreated
                          FROM [dbo].[Books] WHERE DateDeleted IS NULL AND BookId = @BookId;";

        var book = await dbConnection.QuerySingleOrDefaultAsync<BookDto>(sql, new { BookId = id.Value }, transactionToUse);

        if(book is null)
            return null;

        Author? author = null;
        if( book.AuthorId is not null)
        {
             author = await _authorRepository.GetAuthorAsync(AuthorId.Create(book.AuthorId.Value), dbConnection, transactionToUse);
        }

        Publisher? publisher = null;
        if(book.PublisherId is not null)
        {
            publisher = await _publisherRepository.GetPublisherAsync(PublisherId.Create(book.PublisherId.Value), dbConnection, transactionToUse);
        }

        BookFormat? bookFormat = null;
        if(book.BookFormatId is not null)
        {
            bookFormat = await _bookFormatRepository.GetBookFormatAsync(BookFormatId.Create(book.BookFormatId.Value), dbConnection, transactionToUse);
        }

        ProgrammingLanguage? programmingLanguage = null;
        if(book.ProgrammingLanguageId is not null)
        {
            programmingLanguage = await _programmingLanguageRepository.GetProgrammingLanguageAsync(ProgrammingLanguageId.Create(book.ProgrammingLanguageId.Value), dbConnection, transactionToUse);
        }

        transactionToUse.Commit();
        return BookDto.ToBook(book, author, publisher, bookFormat, programmingLanguage);
    }

    public async Task<bool> UpdateBookAsync(Book book, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"UPDATE [dbo].[Books]
                          SET Title = @Title, AuthorId = @AuthorId, PublisherId = @PublisherId, BookFormatId = @BookFormatId, 
                          ProgrammingLanguageId = @ProgrammingLanguageId, ISBN = @Isbn, PublicationYear = @PublicationYear, 
                          Genre =  @Genre, Website = @Website, Notes = @Notes
                          WHERE DateDeleted IS NULL AND BookId = @BookId;";

        var rowsEffected = await dbConnection.ExecuteAsync(sql, new
        {
            book.Title,
            AuthorId = book.AuthorId?.Value,
            PublisherId = book.PublisherId?.Value,
            ProgrammingLanguageId = book.ProgrammingLanguageId?.Value,
            BookFormatId = book.BookFormatId?.Value,
            book.Isbn,
            book.PublicationYear,
            book.Genre,
            book.Website,
            book.Notes,
            BookId = book.Id.Value
        }, transaction);

        return rowsEffected > 0;
    }
}

internal class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int? AuthorId { get; set; }
    public AuthorDto Author { get; set; }
    public int? PublisherId { get; set; }
    public PublisherDto Publisher { get; set; }
    public int? BookFormatId { get; set; }
    public BookFormatDto BookFormat { get; set; }
    public int? ProgrammingLanguageId { get; set; }
    public ProgrammingLanguageDto ProgrammingLanguage { get; set; }
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
            dto.AuthorId == null ? null : Domain.BookAggregate.ValueObjects.AuthorId.Create(dto.AuthorId.Value),
            dto.PublisherId == null ? null : Domain.BookAggregate.ValueObjects.PublisherId.Create(dto.PublisherId.Value),
            dto.BookFormatId == null ? null : Domain.BookAggregate.ValueObjects.BookFormatId.Create(dto.BookFormatId.Value),
            dto.ProgrammingLanguageId == null ? null : Domain.BookAggregate.ValueObjects.ProgrammingLanguageId.Create(dto.ProgrammingLanguageId.Value),
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