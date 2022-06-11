using Dapper;
using Dapper.Contrib.Extensions;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using GateWayAPI.Models.GateWay.Product;
using GateWayAPI.Models.GateWay.ProductOption;
using GateWayAPI.Models.GateWay.ProductType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Repository
{
    public class ProductRepository : SqlRepository, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString) { }

        public IEnumerable<Product> GetAllProduct() {
            using (var conn = GetOpenConnection())
            {
                return conn.GetAll<Product>();
            }
        }
        public IEnumerable<Product> GetProductByType(int ProductTypeId) {
            using (var conn = GetOpenConnection())
            {
                string sql = "SELECT * FROM Product WHERE ProductTypeId=@ProductTypeId";
                var parameters = new DynamicParameters();
                parameters.Add("@ProductTypeId", ProductTypeId, DbType.Int32);
                return conn.Query<Product>(sql, parameters);
            }
        }

        public IEnumerable<ProductType> GetAllProductType() {
            using (var conn = GetOpenConnection())
            {
                return conn.GetAll<ProductType>().Where(x => x.IsActive == true);
            }
        }

        public IEnumerable<ProductOption> GetProductOptionByProductId(int ProductId) {
            using (var conn = GetOpenConnection())
            {
                return conn.GetAll<ProductOption>().Where(x => x.ProductId == ProductId);
            }
        }
    }
}
