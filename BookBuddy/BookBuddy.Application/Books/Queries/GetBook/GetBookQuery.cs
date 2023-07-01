using BookBuddy.Domain.BookAggregate;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using MediatR;

namespace BookBuddy.Application.Books.Queries.GetBook;

public record GetBookQuery(BookId Id) : IRequest<Book?>;
