using BookBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Domain.ProgrammingLanguage;
public class ProgrammingLanguageId : ValueObject
{
    public int Value { get; }

    private ProgrammingLanguageId(int programmingLanugageId)
    {
        Value = programmingLanugageId;
    }

    public static ProgrammingLanguageId Create(int programmingLanugageId)
    {
        return new ProgrammingLanguageId(programmingLanugageId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
