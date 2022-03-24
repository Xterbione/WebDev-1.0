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
                    "Uid=root;Pwd=pmHT*haW6r32LKJ#;"
                    // Svens pw: pmHT*haW6r32LKJ#
                    // Ahmads pw: 1234
                    // Ohter pw: 
                    );
        }
    }
}
