using System.Data;

namespace TurfMate.Server.Services
{
    public interface IDatabaseService
    {
        IDbConnection CreateConnection();
    }
}