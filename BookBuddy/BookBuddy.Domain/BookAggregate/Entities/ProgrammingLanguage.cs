using BookBuddy.Domain.BookAggregate.ValueObjects;
using BookBuddy.Domain.Common;

namespace BookBuddy.Domain.BookAggregate.Entities;

public class ProgrammingLanguage : Entity<ProgrammingLanguageId>
{
    private ProgrammingLanguage(ProgrammingLanguageId programmingLanguageId,
        string language,
        DateTime dateCreated) : base(programmingLanguageId)
    {
        ProgrammingLanguageId = programmingLanguageId;
        Language = language;
        DateCreated = dateCreated;
    }

    public ProgrammingLanguageId ProgrammingLanguageId { get; }
    public string Language { get; set; } = default!;
    public DateTime DateCreated { get; set; }
}
