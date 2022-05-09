using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.CSM.User
{
    public class User
    {
        public string ID { set; get; }
        public int UserId { set; get; }
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string City { set; get; }
        public string State { set; get; }
        public string ZipCode { set; get; }
        public decimal Debit { set; get; }
        public decimal CreditLimit { set; get; }
        public int Active { set; get; }
        public DateTime RecordDate { set; get; }
        public DateTime ExpiryDate { set; get; }
        public int UserType { set; get; }
        public string Memo { set; get; }
        public DateTime BirthDate { set; get; }
        public string SSN1 { set; get; }
        public string SSN2 { set; get; }
        public string SSN3 { set; get; }
        public int TimePaid { set; get; }
        public int TimeUsed { set; get; }
        public int MoneyPaid { set; get; }
        public int MoneyUsed { set; get; }
        public int RemainTime { set; get; }
        public int FreeTime { set; get; }
        public int TimeTransfer { set; get; }
        public int RemainMoney { set; get; }
        public int FreeMoney { set; get; }
        public int MoneyTransfer { set; get; }
        public int Money { set; get; }
        public string MachineName { set; get; }
        public string IP { set; get; }

    }
}
