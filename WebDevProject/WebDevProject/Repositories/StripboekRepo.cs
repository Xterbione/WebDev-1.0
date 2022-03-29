using Dapper;
using System.Data;
using NuGet.Protocol.Plugins;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories
{
    public class StripboekRepo
    {
        string sql = "SELECT * " +
                     "FROM stripboek " +
                     "INNER JOIN serie on stripboek.serie_id = serie.serieId " +
                     "INNER JOIN genre on stripboek.genre_id = genre.genreId";

        private static IDbConnection GetConnection()
        {
            return new DBUtils().GetDbConnection();
        }


        /// <summary>
        /// gets all books
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StripboekModel> Get()
        {
            using var connection = GetConnection();
            //normal query for list
            var strips = connection.Query<StripboekModel, SerieModel, GenreModel, StripboekModel>(sql,
                (StripBoekModel, SerieModel, GenreModel) =>
                {
                    //vul het main object met de aangemaakte objecten
                    StripBoekModel.SerieModel = SerieModel;
                    StripBoekModel.GenreModel = GenreModel;
                    return StripBoekModel;
                }, splitOn: "SerieId, GenreId");
            return strips;
        }

        /// <summary>
        /// Gets stripboekmodel for a strip in the database based on it's stripId
        /// </summary>
        /// <param name="stripId"></param>
        /// <returns></returns>
        public StripboekModel Get(int stripId)
        {
            sql = "SELECT * FROM Stripboek WHERE stripboekId = @stripId";
            using var connection = GetConnection();
            StripboekModel strip = connection.QuerySingle<StripboekModel>(sql, new {stripId});
            return strip;
        }

        /// <summary>
        /// updates a specific book
        /// </summary>
        /// <param name="genreid"></param>
        /// <param name="titel"></param>
        /// <param name="paginas"></param>
        /// <param name="volgnummer"></param>
        /// <param name="serieid"></param>
        /// <param name="boekid"></param>
        public void update(int genreid, string titel, int paginas, int volgnummer, int serieid, int boekid)
        {
            using var connection = GetConnection();
            var sql =
                @"update stripboek set genre_id = @genreid, stripboektitel = @titel, aantal_paginas = @paginas, volgnummer = @volgnummer, serie_id = @serieid where stripboekId = @boekid";
            var removeSeparate = connection.Execute(sql, new {genreid, titel, paginas, volgnummer, serieid, boekid});
        }

        /// <summary>
        /// deletes a 
        /// </summary>
        /// <param name="bookid"></param>
        public void DeleteSingle(int boekid)
        {
            //the query
            string sql = @"DELETE FROM stripboek WHERE stripboekId = @boekid";
            //the connection
            using var connection = GetConnection();
            //executes query
            connection.Execute(sql, new {boekid});
        }

        /// <summary>
        /// insert a new book
        /// </summary>
        /// <param name="genreid"></param>
        /// <param name="titel"></param>
        /// <param name="paginas"></param>
        /// <param name="volgnummer"></param>
        /// <param name="serieid"></param>
        /// <param name="boekid"></param>
        public void insert(int genreid, string titel, int paginas, int volgnummer, int serieid)
        {
            using var connection = GetConnection();
            var sql = @"
                INSERT INTO stripboek (genre_id, stripboektitel, aantal_paginas, volgnummer, serie_id) 
                VALUES (@genreid, @titel, @paginas, @volgnummer, @serieid );";
            var removeSeparate = connection.Execute(sql, new {genreid, titel, paginas, volgnummer, serieid});
        }
        

        public IEnumerable<StripboekModel> Search(string selected, string search)
        {
            switch (selected)
            {
                case "isbn":
                    sql = sql + "WHERE isbn = @search ";
                    break;
                case "stripboektitel":
                    sql = sql + "WHERE Stripboek.stripboektitel LIKE @search";
                    search = $"%{search}%";
                    break;
                default:
                    sql = sql;
                    break;
            }


            using var connection = GetConnection();
            //normal query for list
            var strips = connection.Query<StripboekModel, SerieModel, GenreModel, StripboekModel>(sql,
                (StripBoekModel, SerieModel, GenreModel) =>
                {
                    //vul het main object met de aangemaakte objecten
                    StripBoekModel.SerieModel = SerieModel;
                    StripBoekModel.GenreModel = GenreModel;
                    return StripBoekModel;
                }, splitOn: "SerieId, GenreId");
            return strips;
        }
    }
}