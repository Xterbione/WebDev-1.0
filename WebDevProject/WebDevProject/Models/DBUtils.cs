using MySql.Data.MySqlClient;
using System.Data;

namespace WebDevProject.Pages
{
    public class DBUtils
    {
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(
                    "Server=127.0.0.1;Port=3306;" +
                    "Database=stripboeken;" +
                    "Uid=root;Pwd=1234;"
                    );
        }
    }
}
