using GateWayAPI.Models.GateWay.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.General.StaffModel
{
    public class StaffModel : Staff
    {
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
    }
}
