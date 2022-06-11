using Dapper;
using Dapper.Contrib.Extensions;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using GateWayAPI.Models.GateWay.Order;
using GateWayAPI.Models.GateWay.OrderDetail;
using GateWayAPI.Models.GateWay.Product;
using GateWayAPI.Models.GateWay.ProductOption;
using GateWayAPI.Models.GateWay.ProductType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Repository
{
    public class OrderRepository : SqlRepository, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString) { }

        public IEnumerable<Orders> GetAllOrder()
        {
            using (var conn = GetOpenConnection())
            {
                return conn.GetAll<Orders>();
            }
        }

        public long InsertOrder(Orders order)
        {
            using (var conn = GetOpenConnection())
            {
                return conn.Insert(order);
            }
        }

        public void InsertOrderDetail(OrderDetail order)
        {
            using (var conn = GetOpenConnection())
            {
                conn.Insert(order);
            }
        }
    }
}
