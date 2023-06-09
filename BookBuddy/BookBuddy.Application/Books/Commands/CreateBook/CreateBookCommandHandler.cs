﻿using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Application.Common.Interfaces.Services;
using BookBuddy.Domain.BookAggregate;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using MediatR;

namespace BookBuddy.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
{
    private readonly IBookRepository _bookRepository;
    private readonly IClock _clock;

    public CreateBookCommandHandler(IBookRepository bookRepository, IClock clock)
    {
        _bookRepository = bookRepository;
        _clock = clock;
    }


    public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Book.Create(
            BookId.Create(0),
            request.Title,
            request.AuthorId == null ? null : AuthorId.Create(request.AuthorId.Value),
            request.PublisherId == null ? null : PublisherId.Create(request.PublisherId.Value),
            request.BookFormatId == null ? null : BookFormatId.Create(request.BookFormatId.Value),
            request.ProgrammingLanguageId == null ? null : ProgrammingLanguageId.Create(request.ProgrammingLanguageId.Value),
            request.Author == null ? null : Author.Create(AuthorId.Create(0), request.Author.FirstName, request.Author.LastName),
            request.Publisher == null ? null : Publisher.Create(PublisherId.Create(0), request.Publisher.Name, request.Publisher.Website, _clock.UtcNow),
            request.BookFormat == null ? null : BookFormat.Create(BookFormatId.Create(0), request.BookFormat.Format, _clock.UtcNow),
            request.ProgrammingLanguage == null ? null : ProgrammingLanguage.Create(ProgrammingLanguageId.Create(0), request.ProgrammingLanguage.Language, _clock.UtcNow),
            request.Isbn,
            request.PublicationYear,
            request.Genre,
            request.Website,
            request.Notes,
            _clock.UtcNow);

        var bookId = await _bookRepository.AddBookAsync(book);
        return await _bookRepository.GetBookAsync(bookId);
    }
}
