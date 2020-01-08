using SellerCenterLazada.Models;
using System.Data;
using Dapper;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SellerCenterLazada.Repositories
{
    public class SellerAccountRepository : BaseRepository
    {
        public bool AddSellerAccount(List<SellerAccount> sellerAccount)
        {
            using (var connection = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@JInput", JsonConvert.SerializeObject(sellerAccount));
                parameters.Add("@ReturnValue", direction: ParameterDirection.ReturnValue);
                connection.Execute("spInsSellerAccounts", parameters, commandType: CommandType.StoredProcedure);
                return 1.Equals(parameters.Get<int>("@ReturnValue"));
            }
        }
    }
}
