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

        public int UpdateComputer(string name, int status) {
            using (var conn = GetOpenConnection())
            {
                var sql = "update Computer set Status = @Status where Name = @Name";
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@Name", name, DbType.String);
                _params.Add("@Status", status, DbType.Int32);
                return conn.Execute(sql, _params);
            }
        }
    }
}
