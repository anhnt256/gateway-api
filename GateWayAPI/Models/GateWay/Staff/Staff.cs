using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Staff
{
    [Table("Staff")]
    public class Staff
    {
        [Key]
        public int Id { set; get; }
        public string FullName { set; get; }
        public string UserName { set; get; }
        public string PasswordHash { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Token { set; get; }
        public bool IsActive { set; get; }
        public bool IsAdmin { set; get; }
        public bool IsAuthTwoFactor { set; get; }
    }
}
