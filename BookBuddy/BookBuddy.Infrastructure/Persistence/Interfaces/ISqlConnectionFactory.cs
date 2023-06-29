using System.Data;

namespace BookBuddy.Infrastructure.Persistence.Interfaces;
internal interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}