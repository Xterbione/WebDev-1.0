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

        public SerieModel Get(int serieId)
        {
            string sql = "SELECT * FROM serie WHERE serieId = @serieId";
            using var connection = GetConnection();
            SerieModel serie = connection.QuerySingle<SerieModel>(sql, new {serieId});
            return serie;
        }

        /// <summary>
        /// deleting a serie 
        /// </summary>
        /// <param name="serieUitdelist"></param>
        public void DeleteSingle(int serieid)
        {
            //the query
            string sql = @"DELETE FROM serie WHERE serieId = @serieid";
            //the connection
            using var connection = GetConnection();
            //executes query
            connection.Execute(sql, new {serieid});
        }
        
        
        /// <summary>
        /// een serie toevoegen 
        /// </summary>
        /// <param name="serie"></param>
        /// <param name="landVanOorsprong"></param>
        /// <param name="eerstePublicatie"></param>
        /// <param name="lopend"></param>
        public void insert(string serie, string landVanOorsprong, DateTime eerstePublicatie, bool lopend)
        {
            using var connection = GetConnection();
            var sql = @"
                INSERT INTO serie (serieTitel, land_van_oorsprong, eerste_publicatie, lopend) 
                VALUES (@serie,@landVanOorsprong,@eerstePublicatie,@lopend)";
            var removeSeparate = connection.Execute(sql, new {serie, landVanOorsprong,eerstePublicatie,lopend});
        }
        
        
        /// <summary>
        /// een serie updaten 
        /// </summary>
        /// <param name="serieId"></param>
        /// <param name="serie"></param>
        /// <param name="landVanOorsprong"></param>
        /// <param name="eerstePublicatie"></param>
        /// <param name="lopend"></param>
        public void Update(int serieId, string serie, string landVanOorsprong, string eerstePublicatie, bool lopend)
        {
            using var connection = GetConnection();
            var sql = @"
                UPDATE serie SET serieTitel=@serie,
                land_van_oorsprong=@landVanOorsprong,
                eerste_publicatie=@eerstePublicatie,
                lopend=@lopend
                WHERE serieId=@serieId";
            var removeSeparate = connection.Execute(sql, new {serieId,serie, landVanOorsprong,eerstePublicatie,lopend});
        }
        
        
        public IEnumerable<SerieModel> GetById(int serieId)
        {
            string sql = "SELECT * " +
                         "FROM Serie where serieId=@serieID";


            using var connection = GetConnection();
            //normal query for list
            var strips = connection.Query<SerieModel>(sql, new {serieId});
            return strips;
        }

    }
}
