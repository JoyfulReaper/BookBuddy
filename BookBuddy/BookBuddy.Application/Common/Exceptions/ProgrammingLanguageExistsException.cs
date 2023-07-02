namespace BookBuddy.Application.Common.Exceptions;

public class ProgrammingLanguageExistsException : Exception
{
    public ProgrammingLanguageExistsException()
    {
    }

    public ProgrammingLanguageExistsException(string message)
        : base(message)
    {
    }

    public ProgrammingLanguageExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}