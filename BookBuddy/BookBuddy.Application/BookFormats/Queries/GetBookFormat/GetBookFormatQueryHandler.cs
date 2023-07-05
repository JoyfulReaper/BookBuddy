
using BookBuddy.Application.Common.Exceptions;
using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using MediatR;

namespace BookBuddy.Application.BookFormats.Queries.GetBookFormat;

internal class GetBookFormatQueryHandler : IRequestHandler<GetBookFormat, BookFormat>
{
    private readonly IBookFormatRepository _bookFormatRepository;

    public GetBookFormatQueryHandler(IBookFormatRepository bookFormatRepository)
    {
        _bookFormatRepository = bookFormatRepository;
    }

    public async Task<BookFormat> Handle(GetBookFormat request, CancellationToken cancellationToken)
    {
        var format = await _bookFormatRepository.GetBookFormatAsync(request.BookFormatId);
        if (format is null)
            throw new BookFormatNotFoundException($"Unable to look up BookFormat with Id: {request.BookFormatId.Value}");

        return format;
    }
}
