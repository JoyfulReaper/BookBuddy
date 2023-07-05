using BookBuddy.Domain.BookAggregate.Entities;
using MediatR;

namespace BookBuddy.Application.BookFormats.Queries.GetAllBookFormats;

public record GetAllBookFormatsQuery() : IRequest<IEnumerable<BookFormat>>;