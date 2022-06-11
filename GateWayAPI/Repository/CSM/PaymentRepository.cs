using Dapper;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository.CSM;
using GateWayAPI.Models.CSM.Payment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Repository.CSM
{
    public class PaymentRepository : SqlRepository, IPaymentRepository
    {
        public PaymentRepository(string connectionString) : base(connectionString) { }

        public async Task<IEnumerable<Payment>> GetPaymentByUserId(int userId)
        {
            using (var conn = GetMySQLOpenConnection())
            {
                var sql = "select * from paymenttb where userId = @userId";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId, System.Data.DbType.Int32);
                return await conn.QueryAsync<Payment>(sql, parameters);
            }
        }

        public async Task<IEnumerable<Payment>> GetTopByType(int count, int paymentType = 4)
        {
            using (var conn = GetMySQLOpenConnection())
            {
                var sql = $"SELECT userId, SUM(amount) totalAmount FROM paymenttb" +
                    $" WHERE paymentType = @paymentType " +
                    $" GROUP BY userId " +
                    $"ORDER BY SUM(amount) DESC LIMIT @count";
                var parameters = new DynamicParameters();
                parameters.Add("@count", count, System.Data.DbType.Int32);
                parameters.Add("@paymentType", paymentType, System.Data.DbType.Int32);
                return await conn.QueryAsync<Payment>(sql, parameters);
            }
        }

        public decimal GetTotalPaymentByUserId(int userId)
        {
            string StartGame = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Config")["StartGame"];
            string EndGame = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Config")["EndGame"];
            using (var conn = GetMySQLOpenConnection())
            {
                var sql = "select sum(amount) as amount from paymenttb " +
                    "where userId = @userId and paymentType = 4 and (VoucherNo is null or VoucherNo = '') " +
                    "and (serveDate between cast('" + StartGame + "' as date) and CAST('" + EndGame + "' as date))";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId, System.Data.DbType.Int32);
                return conn.ExecuteScalar(sql, parameters) is null ? 0 : (decimal)conn.ExecuteScalar(sql, parameters);
            }
        }
    }
}
