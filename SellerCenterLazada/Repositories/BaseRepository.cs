using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SellerCenterLazada.Helpers;

namespace SellerCenterLazada.Repositories
{
    public class BaseRepository
    {
        string connectString = ConfigurationManager.AppSettings["License"].ToString();
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(CryptoHelper.Decrypt(connectString));
        }

        public bool CheckSqlConnect()
        {
            try
            {
                using(var connection = GetSqlConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
