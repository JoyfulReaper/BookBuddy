﻿using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IBookFormatRepository
{
    Task<BookFormat> GetBookFormatAsync(BookFormatId id);
    Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync();
    Task<BookFormatId> AddBookFormatAsync(BookFormat bookFormat, IDbTransaction? transaction);
    Task UpdateBookFormatAsync(BookFormat bookFormat, IDbTransaction? transaction);
    Task DeleteBookFormatAsync(BookFormatId id);
}
