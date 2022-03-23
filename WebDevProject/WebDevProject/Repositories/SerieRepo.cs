using Dapper;
using System.Data;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories
{
    public class SerieRepo
    {
        private static IDbConnection GetConnection()
        {
            return new DBUtils().GetDbConnection();
        }



        public IEnumerable<SerieModel> Get()
        {
            string sql = "SELECT * " +
                "FROM Serie ";


            using var connection = GetConnection();
            //normal query for list
            var strips = connection.Query<SerieModel>(sql);
            return strips;
        }
    }
}
