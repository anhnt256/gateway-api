using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Material
{
    [Table("Material")]
    public class Material
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
    }
}
