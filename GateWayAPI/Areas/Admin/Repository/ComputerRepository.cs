using Dapper;
using Dapper.Contrib.Extensions;
using GateWayAPI.Areas.Admin.IRepository;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.Models.GateWay.Computer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Repository
{
    public class ComputerRepository : SqlRepository, IComputerRepository
    {
        public ComputerRepository(string connectionString) : base(connectionString) { }

        public List<Computer> GetComputerStatus()
        {
            using (var conn = GetMySQLOpenConnection())
            {
                var sql = "select distinct machinename as name, status, userid from (select machinename, status, userid from systemlogtb order by concat(EnterDate, ' ', EnterTime) desc) as system group by machinename;";
                return conn.Query<Computer>(sql).ToList();
            }
        }
    }
}
