using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository
{
    public interface IAccountRepository
    {
        Computer GetRealIpFromLocalIp(string ip);

        Task<Account> SelectUserFromId(int AccountId);
        void InsertNewUser(Account account);

        void UpdateUser(Account account);
    }
}
