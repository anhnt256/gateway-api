using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Recipe
{
    [Table("Recipe")]
    public class Recipe
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public bool IsActive { set; get; }
    }
}
