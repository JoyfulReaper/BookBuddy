using BookBuddy.Domain.BookAggregate.ValueObjects;
using MediatR;

namespace BookBuddy.Application.Books.Commands.DeleteBook;

public record DeleteBookCommand(BookId bookId) : IRequest<bool>;
