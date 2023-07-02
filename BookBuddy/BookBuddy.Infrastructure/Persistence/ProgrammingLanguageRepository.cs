using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Domain.BookAggregate.Entities;
using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Infrastructure.Persistence.Interfaces;
using Dapper;
using System.Data;


namespace BookBuddy.Infrastructure.Persistence;
internal class ProgrammingLanguageRepository : IProgrammingLanguageRepository, IDisposable
{
    private readonly IDbConnection _connection;

    public ProgrammingLanguageRepository(ISqlConnectionFactory sqlConnectionFactory, IDbTransaction? transaction)
    {
        _connection = sqlConnectionFactory.CreateConnection();
    }

    public async Task<ProgrammingLanguageId> AddProgrammingLanguageAsync(ProgrammingLanguage programmingLanguage, IDbTransaction? transaction)
    {
        var sql = @"INSERT INTO [dbo].[ProgrammingLanguages]
                            ([Language])
                          VALUES
                            (@Language)
                          SELECT CAST(SCOPE_IDENTITY() AS INT)";

        var programmingLanguageId = await _connection.ExecuteScalarAsync<int>(sql,
            new
            {
                programmingLanguage.Language
            }, transaction);

        return ProgrammingLanguageId.Create(programmingLanguageId);
    }

    public Task DeleteProgrammingLanguageAsync(ProgrammingLanguageId id, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public Task<IEnumerable<ProgrammingLanguage>> GetAllProgrammingLanguagesAsync(IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }

    public Task<ProgrammingLanguage> GetProgrammingLanguageAsync(ProgrammingLanguageId id, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProgrammingLanguageAsync(ProgrammingLanguage author, IDbTransaction? transaction)
    {
        throw new NotImplementedException();
    }
}
