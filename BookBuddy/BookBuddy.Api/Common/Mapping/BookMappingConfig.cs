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

        config.NewConfig<Author, AuthorResponse>()
            .Map(dest => dest.AuthorId, src => src.Id.Value);

        config.NewConfig<Publisher, PublisherResponse>()
            .Map(dest => dest.PublisherId, src => src.Id.Value);

        config.NewConfig<BookFormat, BookFormatResponse>()
            .Map(dest => dest.BookFormatId, src => src.Id.Value);

        config.NewConfig<UpdateBookRequest, UpdateBookCommand>();

        config.NewConfig<ProgrammingLanguage, ProgrammingLanguageResponse>()
            .Map(dest => dest.ProgrammingLanguageId, src => src.Id.Value);

        // TODO: Figure out how to fix the warnings, I don't think this is correct
        // BUT it does work
        config.NewConfig<Book, BookResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Author, src => src.Author)
            .Map(dest => dest.Author.AuthorId, src => src.AuthorId.Value)
            .Map(dest => dest.ProgrammingLanguage.ProgrammingLanguageId, src => src.ProgrammingLanguageId.Value)
            .Map(dest => dest.BookFormat.BookFormatId, src => src.BookFormatId.Value)
            .Map(dest => dest.Publisher.PublisherId, src => src.PublisherId.Value);
    }
}
