using BookBuddy.Domain.BookAggregate.Entities;
using MediatR;

namespace BookBuddy.Application.Authors.Queries.GetAllAuthors;

public record GetAllAuthorsQuery() : IRequest<IEnumerable<Author>>;
