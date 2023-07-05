using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using MediatR;

namespace BookBuddy.Application.BookFormats.Queries.GetBookFormat;

public record GetBookFormat(BookFormatId BookFormatId) : IRequest<BookFormat>;