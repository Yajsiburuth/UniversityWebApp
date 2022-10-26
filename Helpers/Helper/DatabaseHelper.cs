using System.Data.SqlClient;

namespace Helpers.Helper
{
    public class DatabaseHelper
    { 
        public SqlConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Dayforce\\Databases\\Data\\UniversityApplication.mdf;Integrated Security=True;Connect Timeout=30");
            return conn;
        }

    }
}
