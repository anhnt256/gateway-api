using GateWayAPI.Models.CSM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository.CSM
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllService();
        Task<IEnumerable<Service>> GetServiceByType(int ServiceGroupId);
    }
}
