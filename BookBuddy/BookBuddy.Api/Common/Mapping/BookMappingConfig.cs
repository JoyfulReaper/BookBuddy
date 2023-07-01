using BookBuddy.Application.Books.Commands.CreateBook;
using BookBuddy.Contracts.Books;
using BookBuddy.Domain.BookAggregate;
using Mapster;

using Author = BookBuddy.Domain.BookAggregate.Entities.Author;
using BookFormat = BookBuddy.Domain.BookAggregate.Entities.BookFormat;
using ProgrammingLanguage = BookBuddy.Domain.BookAggregate.Entities.ProgrammingLanguage;
using Publisher = BookBuddy.Domain.BookAggregate.Entities.Publisher;

namespace BookBuddy.Api.Common.Mapping;

public class BookMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBookRequest, CreateBookCommand>();

        config.NewConfig<Author, AuthorResponse>();

        config.NewConfig<Publisher, PublisherResponse>();

        config.NewConfig<BookFormat, BookFormatResponse>();

        config.NewConfig<ProgrammingLanguage, ProgrammingLanguageResponse>();

        config.NewConfig<Book, BookResponse>();
    }
}
