using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GateWayAPI.DataAccessLayer
{
    public abstract class SqlRepository : ISqlRepository
    {
        private string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            return DbConnection.GetDbConnection(_connectionString);
        }

        public MySqlConnection GetMySQLOpenConnection()
        {
            return DbConnection.GetMySQLDbConnection(_connectionString);
        }
    }
}
