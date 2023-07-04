using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using MediatR;

namespace BookBuddy.Application.Authors.Queries.GetAuthor;
public record GetAuthorQuery(AuthorId AuthorId) : IRequest<Author>;
