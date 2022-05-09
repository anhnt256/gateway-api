using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Controllers
{
    public class GlobalController : Controller
    {
        const string SessionName = "AppId";

        public IActionResult InitApp()
        {
            var appId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString(SessionName, appId);
            var response = HttpContext.Response;
            response.Cookies.Append("Meo", appId, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
            });
            return Ok();
        }

        public IActionResult Auth() {
            return Ok(CheckAppId());
        }

        private bool CheckAppId() {
            bool isAppId = false;
            var request = HttpContext.Request;
            var appId = request.Cookies["Meo"];
            var currentAppId = HttpContext.Session.GetString(SessionName);
            if (appId == currentAppId) {
                isAppId = true;
            }
            return isAppId;
        }

    }
}
