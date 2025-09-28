using System.Data;
using TerfMate.Server.Models;
using TurfMate.Server.Repositories;
using TurfMate.Server.Services;

using Dapper;

namespace TurfMate.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseService _databaseService;

        public UserRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spUsers_GetByEmail";
            return await connection.QueryFirstOrDefaultAsync<User>(
                sql,
                new { Email = email },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<User> CreateAsync(User user)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spUsers_Create";
            var parameters = new DynamicParameters();
            parameters.Add("Email", user.Email);
            parameters.Add("PasswordHash", user.PasswordHash);
            parameters.Add("FirstName", user.FirstName);
            parameters.Add("LastName", user.LastName);
            parameters.Add("Role", user.Role);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

            user.Id = parameters.Get<int>("Id");
            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spUsers_GetById";
            return await connection.QueryFirstOrDefaultAsync<User>(
                sql,
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<List<User>> GetAllAsync()
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spUsers_GetAll";
            var users = await connection.QueryAsync<User>(sql, commandType: CommandType.StoredProcedure);
            return users.ToList();
        }
    }
}