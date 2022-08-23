using GateWayAPI.Areas.Admin.IRepository;
using GateWayManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComputerController : Controller
    {
        private IMachineRepository _machineRepository;

        public ComputerController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllComputer()
        {
            var result = _machineRepository.GetAllComputers();
            return Ok(new { code = ResponseCode.Success, reply = result });
        }
    }
}
