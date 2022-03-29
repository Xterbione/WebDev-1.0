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

        /// <summary>
        /// Gets GenreModel from database
        /// </summary>
        /// <param name="genre_id">Selects GenreId in database table</param>
        /// <returns></returns>
        public GenreModel Get(int genreId)
        {
            string sql = "SELECT * FROM Genre WHERE genreId = @genreId";
            using var connection = GetConnection();
            GenreModel genre = connection.QuerySingle<GenreModel>(sql, new {genreId});
            return genre;
        }
        
        public void DeleteSingle(string? genreUitDeList)
        {
            //the query
            string sql = @"DELETE FROM genre WHERE genre = @genreUitDeList";
            //the connection
            using var connection = GetConnection();
            //executes query
            connection.Execute(sql, new {genreUitDeList});
        }
        
        public void insert(string genre)
        {
            using var connection = GetConnection();
            var sql = @"
                INSERT INTO genre (genre) 
                VALUES (@genre)";
            var removeSeparate = connection.Execute(sql, new {genre});
        }
        public void Update(int genreId, string genre)
        {
            using var connection = GetConnection();
            var sql = @"
                UPDATE genre SET genre=@genre WHERE genreId=@genreId";
            var removeSeparate = connection.Execute(sql, new {genre, genreId});
        }
        
    }
}