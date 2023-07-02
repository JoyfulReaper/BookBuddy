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

    public ProgrammingLanguageRepository(IDbConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.CreateConnection();
    }

    public async Task<ProgrammingLanguageId> AddProgrammingLanguageAsync(ProgrammingLanguage programmingLanguage, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"INSERT INTO [dbo].[ProgrammingLanguages]
                            ([Language])
                          VALUES
                            (@Language)
                          SELECT CAST(SCOPE_IDENTITY() AS INT)";

        var programmingLanguageId = await dbConnection.ExecuteScalarAsync<int>(sql,
            new
            {
                programmingLanguage.Language
            }, transaction);

        return ProgrammingLanguageId.Create(programmingLanguageId);
    }

    public async Task<bool> DeleteProgrammingLanguageAsync(ProgrammingLanguageId id, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;
        var isProgrammingLanguageAssignedQuery = "SELECT COUNT(*) FROM [dbo].[Books] WHERE [ProgrammingLanguageId] = @ProgrammingLanguageId";
        var isProgrammingLangaugeAssigned = await dbConnection.ExecuteScalarAsync<int>(isProgrammingLanguageAssignedQuery, new { id }, transaction);

        if (isProgrammingLangaugeAssigned > 0)
        {
            throw new Exception("Cannot delete programming language that is assigned to books.");
        }

        var sql = @"DELETE FROM [dbo].[ProgrammingLanguages]
                          WHERE [Id] = @Id";

        return (await dbConnection.ExecuteAsync(sql, new { id }, transaction)) > 0;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public async Task<IEnumerable<ProgrammingLanguage>> GetAllProgrammingLanguagesAsync(IDbConnection connection,
        IDbTransaction? transaction)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"SELECT ProgrammingLanguageId, Language, DateCreated
                          FROM [dbo].[ProgrammingLanguages];";

        var langs = await dbConnection.QueryAsync<ProgrammingLanguageDto>(sql, null, transaction);
        var output = new List<ProgrammingLanguage>();
        foreach (var lang in langs)
        {
            var langToAdd = ProgrammingLanguageDto.ToProgrammingLanguage(lang);
            if (langToAdd is not null)
                output.Add(langToAdd);
        }

        return output;
    }

    public async Task<ProgrammingLanguage?> GetProgrammingLanguageAsync(ProgrammingLanguageId id, 
        IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var dbConnection = connection ?? _connection;

        var sql = @"SELECT ProgrammingLanguageId, Language, DateCreated
                      FROM [dbo].[ProgrammingLanguages]
                     WHERE [ProgrammingLanguageId] = @ProgrammingLanguageId";

        var lang = await dbConnection.QuerySingleOrDefaultAsync<ProgrammingLanguageDto>(sql, new { id }, transaction);
        return ProgrammingLanguageDto.ToProgrammingLanguage(lang);
    }

    public async Task UpdateProgrammingLanguageAsync(ProgrammingLanguage lang, 
        IDbConnection? connection,
        IDbTransaction? transaction)
    {
        var dbConnection = connection ?? _connection;
        var sql = @"UPDATE [dbo].[ProgrammingLanguages]
                       SET [Language] = @Language
                     WHERE [ProgrammingLanguageId] = @ProgrammingLanguageId";

        await dbConnection.ExecuteAsync(sql, new { lang.Language, ProgrammingLanguageId = lang.Id }, transaction);
    }
}

internal class ProgrammingLanguageDto
{
    public int ProgrammingLanguageId { get; set; }
    public string Language { get; set; } = default!;
    public DateTime DateCreated { get; set; }

    public static ProgrammingLanguage? ToProgrammingLanguage(ProgrammingLanguageDto? dto)
    {
        if (dto is null)
        {
            return null;
        }

        return ProgrammingLanguage.Create(Domain.BookAggregate.ValueObjects.ProgrammingLanguageId.Create(dto.ProgrammingLanguageId),
            dto.Language,
            dto.DateCreated);
    }
}