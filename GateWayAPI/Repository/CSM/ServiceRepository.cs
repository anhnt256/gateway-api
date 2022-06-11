using Dapper;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository.CSM;
using GateWayAPI.Models.CSM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Repository.CSM
{
    public class ServiceRepository : SqlRepository, IServiceRepository
    {
        public ServiceRepository(string connectionString) : base(connectionString) { }

        public async Task<IEnumerable<Service>> GetAllService()
        {
            using (var conn = GetMySQLOpenConnection())
            {
                var sql = "select * from servicetb where Active = 1";
                return await conn.QueryAsync<Service>(sql);
            }
        }

        public async Task<IEnumerable<Service>> GetServiceByType(int ServiceGroupId)
        {
            using (var conn = GetMySQLOpenConnection())
            {
                string sql = "select * from servicetb where Active = 1 and ServiceGroupId = @ServiceGroupId";
                var parameters = new DynamicParameters();
                parameters.Add("@ServiceGroupId", ServiceGroupId, System.Data.DbType.Int32);
                return await conn.QueryAsync<Service>(sql, parameters);
            }
        }
    }
}
