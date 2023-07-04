using BookBuddy.Application.Common.Interfaces.Persistence;
using MediatR;

namespace BookBuddy.Application.Books.Commands.DeleteBook;
internal class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        return await _bookRepository.DeleteBookAsync(request.bookId);
    }
}
