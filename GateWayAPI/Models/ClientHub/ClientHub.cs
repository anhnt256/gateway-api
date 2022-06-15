using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayAPI.Areas.Admin.ModelHub;
using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.OrderDetail;
using GateWayAPI.Models.GateWay.ProductOption;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace GateWayAPI.Models.ClientHub
{
    public class ClientHub : Hub
    {
        private IHubContext<ClientHub> _hub;
        private IHubContext<AdminHub> _adminHub;
        private IOrderRepository _orderRepository;

        public ClientHub(IHubContext<ClientHub> hub, IHubContext<AdminHub> adminHub, IOrderRepository orderRepository)
        {
            _hub = hub;
            _adminHub = adminHub;
            _orderRepository = orderRepository;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("Message", "Connected successfully!");
        }

        public async Task UserOrder()
        {
            var orders = _orderRepository.GetAllOrder();
            if (orders.Count > 0) {
                foreach (var order in orders)
                {
                    order.Details = JsonConvert.DeserializeObject<List<OrderDetail>>(order.OrderDetail);
                    if (order.Details.Count > 0) {
                        foreach (var detail in order.Details)
                        {
                            if (!string.IsNullOrEmpty(detail.JsonOptions))
                            {
                                detail.Options = JsonConvert.DeserializeObject<List<ProductOption>>(detail.JsonOptions);
                            }
                        }
                    }
                }
            }
            await _adminHub.Clients.All.SendAsync("UserOrder", orders);
        }
    }
}
