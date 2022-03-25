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
        /// <summary>
        /// Gets SerieModel from database
        /// </summary>
        /// <param name="serieId">Selects GenreId in database table</param>
        /// <returns></returns>
        public SerieModel Get(int serieId)
        {
            string sql = "SELECT * FROM Serie WHERE serieId = @serieId";
            using var connection = GetConnection();
            SerieModel serie = connection.QuerySingle<SerieModel>(sql, new {serieId});
            return serie;
        }
    }
}
