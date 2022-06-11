using GateWayAPI.Models.GateWay.Order;
using GateWayAPI.Models.GateWay.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Orders> GetAllOrder();
        long InsertOrder(Orders order);
        void InsertOrderDetail(OrderDetail order);

    }
}
