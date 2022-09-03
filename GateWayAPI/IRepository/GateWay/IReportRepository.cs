using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using GateWayAPI.Models.GateWay.Product;
using GateWayAPI.Models.GateWay.ProductOption;
using GateWayAPI.Models.GateWay.ProductType;
using GateWayAPI.Models.GateWay.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository
{
    public interface IReportRepository
    {
        Report GetReportInfo(string FromDate, string ToDate, int StaffId);
    }
}
