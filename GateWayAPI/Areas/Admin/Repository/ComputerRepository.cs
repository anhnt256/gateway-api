using Dapper;
using GateWayAPI.Areas.Admin.IRepository;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.Models.GateWay.Computer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Repository
{
    public class ComputerRepository : SqlRepository, IComputerRepository
    {
        public ComputerRepository(string connectionString) : base(connectionString) { }

        public IEnumerable<Computer> GetComputerStatus()
        {
            using (var conn = GetMySQLOpenConnection())
            {
                var sql = "select distinct(machineName), log.userid, log.status, log.IpAddress as 'realIP', machinename as name from systemlogtb log inner join clientsystb clientSys on clientSys.CPName = log.MachineName group by log.machinename order by enterDate desc;";
                return conn.Query<Computer>(sql);
            }
        }
    }
}
