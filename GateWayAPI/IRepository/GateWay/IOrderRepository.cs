using GateWayAPI.Models.GateWay.Order;
using GateWayAPI.Models.GateWay.OrderDetail;
using GateWayAPI.Models.General.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository
{
    public interface IOrderRepository
    {
        List<OrderModel> GetAllOrder();
        long InsertOrder(Orders order);
        void InsertOrderDetail(OrderDetail order);
        bool UpdateOrder(Orders order);
    }
}
