using BookBuddy.Domain.BookAggregate;
using MediatR;

namespace BookBuddy.Application.Books.Queries.GetAllBooks;

public record GetAllBooksQuery() : IRequest<IEnumerable<Book>>;