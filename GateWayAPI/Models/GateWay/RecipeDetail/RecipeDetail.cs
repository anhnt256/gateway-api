using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.RecipeDetail
{
    [Table("RecipeDetail")]
    public class RecipeDetail
    {
        [Key]
        public int Id { set; get; }
        public int RecipeId { set; get; }
        public int MaterialId { set; get; }
        public int Amount { set; get; }
        public int IsActive { set; get; }
    }
}
