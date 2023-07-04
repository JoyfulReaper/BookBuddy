using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using MediatR;


namespace BookBuddy.Application.Authors.Queries.GetAllAuthors;
internal class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var allAuthors = _authorRepository.GetAllAuthorsAsync();
        return allAuthors;
    }
}
