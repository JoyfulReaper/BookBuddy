using AutoMapper;
using BookBuddy.Server.Data;
using BookBuddy.Shared.Contracts;

namespace BookBuddy.Server.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookResponse>();
        CreateMap<CreateBookRequest, Book>();
    }
}
