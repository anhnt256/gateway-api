using GateWayAPI.Areas.Admin.IRepository;
using GateWayAPI.Controllers;
using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.General.StaffModel;
using GateWayManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Controllers
{
    public class StaffController : GlobalController
    {
        private IStaffRepository _staffRepo;

        public StaffController(IStaffRepository staffRepo)
        {
            _staffRepo = staffRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Authenticate([FromBody] StaffModel staff)
        {
            var result = _staffRepo.Authenticate(staff.UserName.ToLower(), CreatePasswordHash(staff.Password));
            if (result != null)
            {
                return Ok(new { code = ResponseCode.Success, reply = result });
            }
            else
            {
                return Ok(new { code = ResponseCode.ParamsInvalid, reply = "Tên tài khoản hoặc mật khẩu không đúng." });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword([FromBody] StaffModel staff)
        {
            var result = _staffRepo.Authenticate(staff.UserName.ToLower(), CreatePasswordHash(staff.Password));
            if (result != null)
            {
                var response = _staffRepo.ChangePassword(staff.UserName.ToLower(), CreatePasswordHash(staff.ConfirmPassword));
                if (response == true)
                {
                    return Ok(new { code = ResponseCode.Success, reply = "Đổi mật khẩu thành công. Vui lòng đăng nhập lại với mật khẩu mới." });
                }
                else {
                    return Ok(new { code = ResponseCode.ParamsInvalid, reply = "Đổi mật khẩu không thành công" });
                }
            }
            else
            {
                return Ok(new { code = ResponseCode.ParamsInvalid, reply = "Mật khẩu hiện tại không đúng." });
            }
        }

        private string CreatePasswordHash(string password)
        {
            var saltPassword = Encoding.ASCII.GetBytes(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("GeneralSetting")["Secret"]);
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                  password: password,
                  salt: saltPassword,
                  prf: KeyDerivationPrf.HMACSHA1,
                  iterationCount: 10000,
                  numBytesRequested: 256 / 16));
            return hashedPassword;
        }
    }
}
