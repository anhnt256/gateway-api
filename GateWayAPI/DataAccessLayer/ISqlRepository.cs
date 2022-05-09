using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.DataAccessLayer
{
    public interface ISqlRepository
    {
        IDbConnection GetOpenConnection();
        MySqlConnection GetMySQLOpenConnection();
    }
}
