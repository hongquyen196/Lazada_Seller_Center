using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SellerCenterLazada.Repositories
{
    public class BaseRepository
    {
        string connectString = ConfigurationManager.ConnectionStrings["SqlConnectString"].ToString();
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectString);
        }
    }
}
