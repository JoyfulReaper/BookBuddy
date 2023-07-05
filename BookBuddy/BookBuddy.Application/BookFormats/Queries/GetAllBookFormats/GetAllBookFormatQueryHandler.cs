using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using MediatR;

namespace BookBuddy.Application.BookFormats.Queries.GetAllBookFormats;
internal class GetAllBookFormatQueryHandler : IRequestHandler<GetAllBookFormatsQuery, IEnumerable<BookFormat>>
{
    private readonly IBookFormatRepository _bookFormatRepository;

    public GetAllBookFormatQueryHandler(IBookFormatRepository bookFormatRepository)
    {
        _bookFormatRepository = bookFormatRepository;
    }

    public async Task<IEnumerable<BookFormat>> Handle(GetAllBookFormatsQuery request, CancellationToken cancellationToken)
    {
        var allFormats = await _bookFormatRepository.GetAllBookFormatsAsync();
        return allFormats;
    }
}
