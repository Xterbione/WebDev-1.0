using Dapper;
using System.Data;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories
{
    public class GenreRepo
    {
        private static IDbConnection GetConnection()
        {
            return new DBUtils().GetDbConnection();
        }


        /// <summary>
        /// gets al genres in a ienumerable list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GenreModel> Get()
        {
            string sql = "SELECT * " +
                "FROM genre ";


            using var connection = GetConnection();
            //normal query for list
            var strips = connection.Query<GenreModel>(sql);
            return strips;
        }
    }
}
