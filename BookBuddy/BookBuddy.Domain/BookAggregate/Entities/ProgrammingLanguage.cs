using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate.Entities;

public class ProgrammingLanguage : Entity<ProgrammingLanguageId>
{
    public string Language { get; } = default!;
    public DateTime DateCreated { get; }

    private ProgrammingLanguage(
        ProgrammingLanguageId programmingLanguageId,
        string language,
        DateTime dateCreated) : base(programmingLanguageId)
    {
        Id = programmingLanguageId;
        Language = language;
        DateCreated = dateCreated;
    }

    public static ProgrammingLanguage Create(
        ProgrammingLanguageId programmingLanguageId,
        string language,
        DateTime dateCreated)
    {
        return new ProgrammingLanguage(
            programmingLanguageId,
            language,
            dateCreated);
    }
}
