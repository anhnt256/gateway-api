using GateWayAPI.IRepository;
using GateWayAPI.IRepository.CSM;
using GateWayAPI.Models.CSM.User;
using GateWayAPI.Models.GateWay.Account;
using GateWayManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Controllers
{
    public class AccountController : GlobalController
    {
        private IAccountRepository _accountRepository;
        private IUserRepository _userRepository;

        public AccountController(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult AutoLogin([FromBody] User localUser)
        {
            var computer = _accountRepository.GetRealIpFromLocalIp(localUser.IP);
            if (computer != null)
            {
                var user = _userRepository.GetUserByIPAsync(computer.RealIp).Result;
                if (user is null)
                {
                    return Ok(new { code = ResponseCode.ParamsInvalid, reply = "" });
                }
                Account customUser = _accountRepository.SelectUserFromId(user.UserId).Result;
                if (customUser is null)
                {
                    Account cus = new Account();
                    cus.Id = user.UserId;
                    cus.UserName = localUser.UserName;
                    _accountRepository.InsertNewUser(cus);
                    return Ok(new { code = ResponseCode.ParamsInvalid, reply = "" });
                }

                if (customUser.UserName == null)
                {
                    return Ok(new { code = ResponseCode.RequireUserName, reply = "" });
                }

                return Ok(new { code = ResponseCode.Success, reply = customUser });
            }
            return Ok(new { code = ResponseCode.ParamsInvalid, reply = "" });
        }

        [HttpPost]
        public IActionResult UpdateAccount([FromBody] User localUser)
        {
            var computer = _accountRepository.GetRealIpFromLocalIp(localUser.IP);
            if (computer != null)
            {
                var user = _userRepository.GetUserByIPAsync(computer.RealIp).Result;
                if (user is null)
                {
                    return Ok(new { code = ResponseCode.ParamsInvalid, reply = "" });
                }
                Account customUser = _accountRepository.SelectUserFromId(user.UserId).Result;
                if (customUser is null)
                {
                    return Ok(new { code = ResponseCode.ParamsInvalid, reply = "" });
                }
                else
                {
                    customUser.UserName = localUser.UserName;
                    _accountRepository.UpdateUser(customUser);
                }

                return Ok(new { code = ResponseCode.Success, reply = customUser });
            }
            return Ok(new { code = ResponseCode.ParamsInvalid, reply = "" });
        }

    }
}
