using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using System.Data;


namespace BookBuddy.Infrastructure.Persistence;
internal class ProgrammingLanguageRepository : IProgrammingLanguageRepository
{
    public Task<ProgrammingLanguageId> AddProgrammingLanguageAsync(ProgrammingLanguage author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProgrammingLanguageAsync(ProgrammingLanguageId id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProgrammingLanguage>> GetAllProgrammingLanguagesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProgrammingLanguage> GetProgrammingLanguageAsync(ProgrammingLanguageId id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProgrammingLanguageAsync(ProgrammingLanguage author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}
