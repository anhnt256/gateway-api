using GateWayAPI.Models.CSM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository.CSM
{
    public interface IUserRepository
    {
        Task<User> GetUserByIPAsync(string ip);
    }
}
