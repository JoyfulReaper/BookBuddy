namespace BookBuddy.Application.Common.Exceptions;

public class AuthorExistsException : Exception
{
    public AuthorExistsException()
    {
    }

    public AuthorExistsException(string message)
        : base(message)
    {
    }

    public AuthorExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}