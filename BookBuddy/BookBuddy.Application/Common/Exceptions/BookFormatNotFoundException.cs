namespace BookBuddy.Application.Common.Exceptions;

public class BookFormatNotFoundException : Exception
{
    public BookFormatNotFoundException()
    {
    }

    public BookFormatNotFoundException(string message)
        : base(message)
    {
    }

    public BookFormatNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}