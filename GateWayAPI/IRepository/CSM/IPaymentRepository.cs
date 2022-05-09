using GateWayAPI.Models.CSM.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository.CSM
{

    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentByUserId(int userId);
        Task<IEnumerable<Payment>> GetTopByType(int count, int type);
        decimal GetTotalPaymentByUserId(int userId);
    }
}
