using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace SellerCenterLazada.Repositories
{
    public class LicenseRepository : BaseRepository
    {
        public int CheckLicense(string lisence)
        {
            using (var connection = GetSqlConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@License", lisence);
                parameters.Add("@ReturnValue", direction: ParameterDirection.ReturnValue);
                connection.Execute("spCheckLicense", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@ReturnValue");
            }
        }
    }
}
