using Dapper;
using Dapper.Contrib.Extensions;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Repository
{
    public class AccountRepository : SqlRepository, IAccountRepository
    {
        public AccountRepository(string connectionString) : base(connectionString) { }

        public async Task<Account> SelectUserFromId(int AccountId) {
            using (var conn = GetOpenConnection())
            {
                string sql = "SELECT * FROM Account WHERE Id=@AccountId";
                var parameters = new DynamicParameters();
                parameters.Add("@AccountId", AccountId, DbType.Int32);
                var results = await conn.QueryFirstOrDefaultAsync<Account>(sql, parameters);
                return results;
            }
        }

        public void InsertNewUser(Account account)
        {
            using (var conn = GetOpenConnection())
            {
                conn.Insert(account);
            }
        }

        public void UpdateUser(Account account) {
            using (var conn = GetOpenConnection())
            {
                conn.Update(account);
            }
        }

        public Computer GetRealIpFromLocalIp(string ip)
        {
            using (var conn = GetOpenConnection())
            {
                string sql = "SELECT * FROM Computer WHERE Candidate=@Ip";
                var parameters = new DynamicParameters();
                parameters.Add("@Ip", ip, DbType.String);
                var results = conn.QueryFirstOrDefault<Computer>(sql, parameters);
                return results;
            }
        }
    }
}
