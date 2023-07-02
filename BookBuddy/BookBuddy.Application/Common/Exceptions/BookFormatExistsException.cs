namespace BookBuddy.Application.Common.Exceptions;

public class BookFormatExistsException : Exception
{
    public BookFormatExistsException()
    {
    }

    public BookFormatExistsException(string message)
        : base(message)
    {
    }

    public BookFormatExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}