using Dapper;
using System.Data;
using TerfMate.Server.Models;
using TurfMate.Server.Repositories;
using TurfMate.Server.Services;

namespace TerfMate.Server.Repositories
{
    public class TurfRepository : ITurfRepository
    {
        private readonly IDatabaseService _databaseService;

        public TurfRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<Turf>> GetAllAsync()
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spTurfs_GetAll";
            var turfs = await connection.QueryAsync<Turf>(sql, commandType: CommandType.StoredProcedure);
            return turfs.ToList();
        }

        public async Task<Turf?> GetByIdAsync(int id)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spTurfs_GetById";
            return await connection.QueryFirstOrDefaultAsync<Turf>(
                sql,
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Turf> CreateAsync(Turf turf)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spTurfs_Create";
            var parameters = new DynamicParameters();
            parameters.Add("Name", turf.Name);
            parameters.Add("Location", turf.Location);
            parameters.Add("Description", turf.Description);
            parameters.Add("PricePerHour", turf.PricePerHour);
            parameters.Add("ImageUrl", turf.ImageUrl);
            parameters.Add("IsAvailable", turf.IsAvailable);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

            turf.Id = parameters.Get<int>("Id");
            return turf;
        }

        public async Task<Turf?> UpdateAsync(int id, Turf updatedTurf)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spTurfs_Update";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Name", updatedTurf.Name);
            parameters.Add("Location", updatedTurf.Location);
            parameters.Add("Description", updatedTurf.Description);
            parameters.Add("PricePerHour", updatedTurf.PricePerHour);
            parameters.Add("ImageUrl", updatedTurf.ImageUrl);
            parameters.Add("IsAvailable", updatedTurf.IsAvailable);

            var affectedRows = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

            if (affectedRows > 0)
            {
                updatedTurf.Id = id;
                return updatedTurf;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spTurfs_Delete";
            var affectedRows = await connection.ExecuteAsync(
                sql,
                new { Id = id },
                commandType: CommandType.StoredProcedure);

            return affectedRows > 0;
        }

        public async Task<List<Turf>> GetAvailableTurfsAsync()
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spTurfs_GetAvailable";
            var turfs = await connection.QueryAsync<Turf>(sql, commandType: CommandType.StoredProcedure);
            return turfs.ToList();
        }
    }
}