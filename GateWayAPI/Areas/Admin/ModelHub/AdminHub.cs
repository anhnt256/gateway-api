using GateWayAPI.Areas.Admin.IRepository;
using GateWayAPI.IRepository;
using GateWayManagement.Models;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.ModelHub
{
    public class AdminHub : Hub
    {
        private IOrderRepository _orderRepository;
        private IAccountRepository _accountRepository;
        private IComputerRepository _computerRepository;
        private IMachineRepository _machineRepository;
        private IHubContext<AdminHub> _hub;
        public AdminHub(IHubContext<AdminHub> hub, IOrderRepository orderRepository, IComputerRepository computerRepository, IAccountRepository accountRepository, IMachineRepository machineRepository)
        {
            _hub = hub;
            _orderRepository = orderRepository;
            _computerRepository = computerRepository;
            _accountRepository = accountRepository;
            _machineRepository = machineRepository;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            RecurringJob.AddOrUpdate("Update Computer", () => GetComputerAsync(), "*/1 * * * * ");
            await Clients.Caller.SendAsync("Message", "Connected successfully!");
        }

        public async Task GetComputerAsync()
        {
            var computers = _computerRepository.GetComputerStatus();
            var listComputer = computers.ToList();
            for (int i = 0; i < listComputer.Count; i++)
            {
                if (listComputer[i].Status == (int)ComputerStatus.On)
                {
                    var account = _accountRepository.GetAccountById(listComputer[i].UserId);
                    if (account != null)
                    {
                        listComputer[i].UserName = account.UserName;
                    }
                }
            }
            await _hub.Clients.All.SendAsync("ComputerAsync", listComputer);
        }
    }
}
