using System.Configuration;
using System.Data.SqlClient;

namespace Helpers.Helper
{
    public abstract class DatabaseHelper
    {
        protected SqlConnection conn;

        protected DatabaseHelper()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();
        }
    }
}
