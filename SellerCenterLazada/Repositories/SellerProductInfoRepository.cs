using Dapper;
using Newtonsoft.Json;
using SellerCenterLazada.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SellerCenterLazada.Repositories
{
    public class SellerProductInfoRepository : BaseRepository
    {
        public bool InsertSellerProductInfos(List<SellerProductInfo> sellerProductInfos)
        {
            using (var connection = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@JInput", JsonConvert.SerializeObject(sellerProductInfos));
                parameters.Add("@ReturnValue", direction: ParameterDirection.ReturnValue);
                connection.Execute("spInsSellerProductInfos", parameters, commandType: CommandType.StoredProcedure);
                return 1.Equals(parameters.Get<int>("@ReturnValue"));
            }
        } 
        
        public bool UpdateSellerProductInfos(SellerProductInfo sellerProductInfo)
        {
            using (var connection = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@JInput", JsonConvert.SerializeObject(sellerProductInfo));
                parameters.Add("@ReturnValue", direction: ParameterDirection.ReturnValue);
                connection.Execute("spUpdateSellerProductInfo", parameters, commandType: CommandType.StoredProcedure);
                return 1.Equals(parameters.Get<int>("@ReturnValue"));
            }
        }

        public List<SellerProductInfo> GetAllQueue()
        {
            using (var connection = GetSqlConnection())
            {
                var result = connection.Query<SellerProductInfo>("spCheckQueue", commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public void UpdateStatus(long itemId, long skuId, bool status)
        {
            using(var connection = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ItemId", itemId);
                parameters.Add("@SkuId", skuId);
                parameters.Add("@Status", status);
                connection.Execute("spChangeStatusSellerProduct", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        
    }
}
