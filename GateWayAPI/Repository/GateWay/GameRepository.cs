using Dapper;
using Dapper.Contrib.Extensions;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository.GateWay;
using GateWayManagement.Models.GateWay.Game;
using GateWayManagement.Models.GateWay.GameParam;
using GateWayManagement.Models.GateWay.GameResult;
using GateWayManagement.Models.GateWay.AccountGameMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Repository.GateWay
{
    public class GameRepository : SqlRepository, IGameRepository
    {
        public GameRepository(string connectionString) : base(connectionString) { }

        public List<Game> SelectAllGames()
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "Select * from Game";
                return conn.Query<Game>(sql).ToList();
            }
        }

        public List<GameParam> SelectAllGameParam(int gameId)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "Select * from GameParam where gameId = @gameId";
                var parameters = new DynamicParameters();
                parameters.Add("@gameId", gameId, System.Data.DbType.Int32);
                return conn.Query<GameParam>(sql, parameters).ToList();
            }
        }

        public long InsertUserGameMap(AccountGameMap userGameMap)
        {
            using (var conn = GetOpenConnection())
            {
                return conn.Insert(userGameMap);
            }
        }

        public async Task<int> CheckRound(int accountId, int gameId)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "Select round from AccountGameMap where accountId = @accountId and gameId = @gameId";
                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId, System.Data.DbType.Int32);
                parameters.Add("@gameId", gameId, System.Data.DbType.Int32);
                return await conn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            }
        }

        public async Task<AccountGameMap> CheckExist(int accountId, int gameId)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "Select * from AccountGameMap where accountId = @accountId and gameId = @gameId";
                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId, System.Data.DbType.Int32);
                parameters.Add("@gameId", gameId, System.Data.DbType.Int32);
                return await conn.QueryFirstOrDefaultAsync<AccountGameMap>(sql, parameters);
            }
        }

        public int UpdateRound(int accountId, int gameId, int? round)
        {
            using (var conn = GetOpenConnection())
            {
                string sql = string.Empty;
                if (round.HasValue)
                {
                    sql = "update AccountGameMap set round = @round where gameId = @gameId and accountId = @accountId";
                }
                else
                {
                    sql = "update AccountGameMap set round = round - 1 where gameId = @gameId and accountId = @accountId";
                }
                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId, System.Data.DbType.Int32);
                parameters.Add("@gameId", gameId, System.Data.DbType.Int32);
                if (round.HasValue)
                {
                    parameters.Add("@round", round.Value, System.Data.DbType.Int32);
                }
                return conn.Execute(sql, parameters);
            }
        }

        public int CalcRound(int accountId, int gameId)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "update AccountGameMap set round = round - 1 where gameId = @gameId and accountId = @accountId";
                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId, System.Data.DbType.Int32);
                parameters.Add("@gameId", gameId, System.Data.DbType.Int32);
                return conn.Execute(sql, parameters);
            }
        }

        public long InsertEventResult(GameResult result)
        {
            using (var conn = GetOpenConnection())
            {
                return conn.Insert(result);
            }
        }

        public List<GameResult> GetResult(int gameId, int accountId)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "Select e.*, p.name from GameResult e " +
                    "inner join gameparam p on e.paramId = p.id " +
                    "where e.gameId = @gameId and accountId = @accountId " +
                    "order by e.Id DESC";
                var parameters = new DynamicParameters();
                parameters.Add("@gameId", gameId, System.Data.DbType.Int32);
                parameters.Add("@accountId", accountId, System.Data.DbType.Int32);
                return conn.Query<GameResult>(sql, parameters).ToList();
            }
        }

        public bool UseCode(string code)
        {
            using (var conn = GetOpenConnection())
            {
                return conn.Update(new GameResult { Code = code, IsUsed = 1 });
            }
        }
    }
}
