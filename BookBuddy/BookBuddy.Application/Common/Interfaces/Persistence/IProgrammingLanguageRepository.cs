using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IProgrammingLanguageRepository
{
    Task<ProgrammingLanguage> GetProgrammingLanguageAsync(ProgrammingLanguageId id);
    Task<IEnumerable<ProgrammingLanguage>> GetAllProgrammingLanguagesAsync();
    Task<ProgrammingLanguageId> AddProgrammingLanguageAsync(ProgrammingLanguage author, IDbTransaction? transaction);
    Task UpdateProgrammingLanguageAsync(ProgrammingLanguage author, IDbTransaction? transaction);
    Task DeleteProgrammingLanguageAsync(ProgrammingLanguageId id);
}
