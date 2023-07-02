using System.Data;

namespace BookBuddy.Infrastructure.Persistence.Interfaces;
internal interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}