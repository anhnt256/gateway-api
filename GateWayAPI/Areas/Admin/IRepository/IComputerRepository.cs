using GateWayAPI.Models.GateWay.Computer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.IRepository
{
    public interface IComputerRepository
    {
        IEnumerable<Computer> GetComputerStatus();
    }
}
