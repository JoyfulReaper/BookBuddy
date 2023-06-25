using BookBuddy.Application.Common.Interfaces.Services;

namespace BookBuddy.Infrastructure.Services;

public class Clock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
