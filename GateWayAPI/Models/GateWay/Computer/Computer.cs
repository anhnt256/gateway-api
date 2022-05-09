using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Computer
{
    [Table("Computer")]
    public class Computer
    {
        [Key]
        public int Id { set; get; }
        public string Ip { set; get; }
        public string Candidate { set; get; }
        public string Name { set; get; }
        public string RealIp { set; get; }
    }
}
