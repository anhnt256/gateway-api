using GateWayAPI.Models.GateWay.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.IRepository
{
    public interface IStaffRepository
    {
        Staff Authenticate(string username, string password);
        bool ChangePassword(string username, string password);
    }
}
