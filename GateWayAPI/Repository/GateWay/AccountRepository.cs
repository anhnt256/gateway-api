using Dapper;
using Dapper.Contrib.Extensions;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GateWayAPI.Repository
{
    public class AccountRepository : SqlRepository, IAccountRepository
    {
        public AccountRepository(string connectionString) : base(connectionString) { }

        public async Task<Account> Auth(int AccountId)
        {
            using (var conn = GetOpenConnection())
            {
                string sql = "SELECT * FROM Account WHERE Id=@AccountId";
                var parameters = new DynamicParameters();
                parameters.Add("@AccountId", AccountId, DbType.Int32);
                var user = await conn.QueryFirstOrDefaultAsync<Account>(sql, parameters);
                if (user == null)
                    return null;
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("GeneralSetting")["Secret"];
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user;
            }
        }

        public Account GetAccountById(int AccountId)
        {
            using (var conn = GetOpenConnection())
            {
                return conn.Get<Account>(AccountId);
            }
        }

        public Account GetAccountByUserName(string UserName)
        {
            using (var conn = GetOpenConnection())
            {
                return conn.GetAll<Account>().Where(x => x.UserName == UserName).FirstOrDefault();
            }
        }

        public void InsertNewUser(Account account)
        {
            using (var conn = GetOpenConnection())
            {
                string sql = "INSERT INTO Account(Id) VALUES (@Id)";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", account.Id, DbType.Int32);
                conn.Execute(sql, parameters);
            }
        }

        public void UpdateUser(Account account)
        {
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
