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
    public class MachineRepository : SqlRepository, IMachineRepository
    {
        public MachineRepository(string connectionString) : base(connectionString) { }

        public IEnumerable<Computer> GetAllComputers() {
            using (var conn = GetOpenConnection())
            {
                return conn.GetAll<Computer>();
            }
        }
    }
}
