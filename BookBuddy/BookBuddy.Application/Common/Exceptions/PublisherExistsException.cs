namespace BookBuddy.Application.Common.Exceptions;

public class PublisherExistsException : Exception
{
    public PublisherExistsException()
    {
    }

    public PublisherExistsException(string message)
        : base(message)
    {
    }

    public PublisherExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}