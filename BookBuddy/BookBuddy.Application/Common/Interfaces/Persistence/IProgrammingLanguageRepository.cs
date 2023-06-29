using BookBuddy.Domain.BookAggregate.Entities;
using System.Data;

namespace BookBuddy.Application.Common.Interfaces.Persistence;

public interface IProgrammingLanguageRepository
{
    Task<ProgrammingLanguage> GetProgrammingLanguageAsync(int id);
    Task<IEnumerable<ProgrammingLanguage>> GetAllProgrammingLanguagesAsync();
    Task<ProgrammingLanguage> AddProgrammingLanguageAsync(ProgrammingLanguage author, IDbTransaction? transaction);
    Task<ProgrammingLanguage> UpdateProgrammingLanguageAsync(ProgrammingLanguage author, IDbTransaction? transaction);
    Task DeleteProgrammingLanguageAsync(int id);
}
