using Dapper;
using Dapper.Contrib.Extensions;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using GateWayAPI.Models.GateWay.Product;
using GateWayAPI.Models.GateWay.ProductOption;
using GateWayAPI.Models.GateWay.ProductType;
using GateWayAPI.Models.GateWay.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Repository
{
    public class ReportRepository : SqlRepository, IReportRepository
    {
        public ReportRepository(string connectionString) : base(connectionString) { }

        public Report GetReportInfo(string FromDate, string ToDate, int StaffId) {
            using (var conn = GetOpenConnection())
            {
                var sql = "[sp_Admin_GetReportBasicInfo]";
                var parameters = new DynamicParameters();
                parameters.Add("@FromDate", FromDate, DbType.String);
                parameters.Add("@ToDate", ToDate, DbType.String);
                parameters.Add("@StaffId", StaffId, DbType.Int32);
                return conn.Query<Report>(sql, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
