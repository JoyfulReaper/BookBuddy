using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IProgrammingLanguageRepository
{
    Task<ProgrammingLanguage?> GetProgrammingLanguageAsync(ProgrammingLanguageId id, IDbTransaction? transaction = null);
    Task<IEnumerable<ProgrammingLanguage>> GetAllProgrammingLanguagesAsync(IDbTransaction? transaction = null);
    Task<ProgrammingLanguageId> AddProgrammingLanguageAsync(ProgrammingLanguage programmingLanguage, IDbTransaction? transaction = null);
    Task UpdateProgrammingLanguageAsync(ProgrammingLanguage programmingLanguage, IDbTransaction? transaction = null);
    Task<bool> DeleteProgrammingLanguageAsync(ProgrammingLanguageId id, IDbTransaction? transaction = null);
}
