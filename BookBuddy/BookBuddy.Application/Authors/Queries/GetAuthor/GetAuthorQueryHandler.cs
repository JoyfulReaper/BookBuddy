using BookBuddy.Application.Common.Exceptions;
using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using MediatR;

namespace BookBuddy.Application.Authors.Queries.GetAuthor;
internal class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, Author>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Author> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorAsync(request.AuthorId);
        if (author is null)
        {
            throw new AuthorNotFoundException($"Unable to find author with id: {request.AuthorId.Value}");
        }

        return author;
    }
}
