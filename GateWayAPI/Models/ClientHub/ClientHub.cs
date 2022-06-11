using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayAPI.Areas.Admin.ModelHub;
using Hangfire;
using Microsoft.AspNetCore.SignalR;

namespace GateWayAPI.Models.ClientHub
{
    public class ClientHub : Hub
    {
        private IHubContext<ClientHub> _hub;
        private IHubContext<AdminHub> _adminHub;

        public ClientHub(IHubContext<ClientHub> hub, IHubContext<AdminHub> adminHub)
        {
            _hub = hub;
            _adminHub = adminHub;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("Message", "Connected successfully!");
        }

        public async Task UserOrder()
        {
            await _adminHub.Clients.All.SendAsync("UserOrder", "New Order");
        }
    }
}
