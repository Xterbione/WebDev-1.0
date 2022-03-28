using Dapper;
using System.Data;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories
{
    public class DrukRepo
    {
        private static IDbConnection GetConnection()
        {
            return new DBUtils().GetDbConnection();
        }

        string sql = "SELECT * " +
                     "FROM druk " +
                     "INNER JOIN Stripboek on stripboek.stripboekId = druk.stripboek_id " +
                     "INNER JOIN serie on stripboek.serie_id = serie.serieId " +
                     "INNER JOIN genre on stripboek.genre_id = genre.genreId " +
                     "INNER JOIN Uitgever on Druk.uitgever_id = Uitgever.uitgeverId ";


        /// <summary>
        /// Haalt alle drukken van stripboeken op
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DrukModel> Get()
        {
            using var connection = GetConnection();
            //normal query for list
            IEnumerable<DrukModel> strips = connection
                .Query<DrukModel, StripboekModel, SerieModel, GenreModel, UitgeverModel
                    , DrukModel>(sql,
                    (DrukModel, StripboekModel, SerieModel, GenreModel, UitgeverModel) =>
                    {
                        //vul het main object met de aangemaakte objecten
                        StripboekModel.SerieModel = SerieModel;
                        StripboekModel.GenreModel = GenreModel;
                        DrukModel.StripboekModel = StripboekModel;
                        DrukModel.UitgeverModel = UitgeverModel;
                        return DrukModel;
                    }, splitOn: "stripboekId, serieId, genreId, uitgeverId",
                    commandType: CommandType.Text);
            return strips;
        }

        /// <summary>
        /// Gets DrukModel based on DrukId
        /// </summary>
        /// <param name="drukId"></param>
        /// <returns></returns>
        public IEnumerable<DrukModel> Get(int stripboek_id)
        {
            sql = sql + "WHERE stripboek_id = @stripboek_id";
            
            using var connection = GetConnection();
            //normal query for list
            IEnumerable<DrukModel> strips = connection
                .Query<DrukModel, StripboekModel, SerieModel, GenreModel, UitgeverModel
                    , DrukModel>(sql,
                    (DrukModel, StripboekModel, SerieModel, GenreModel, UitgeverModel) =>
                    {
                        //vul het main object met de aangemaakte objecten
                        StripboekModel.SerieModel = SerieModel;
                        StripboekModel.GenreModel = GenreModel;
                        DrukModel.StripboekModel = StripboekModel;
                        DrukModel.UitgeverModel = UitgeverModel;
                        return DrukModel;
                    }, new {stripboek_id}, splitOn: "stripboekId, serieId, genreId, uitgeverId");
            return strips;        
        }

        /// <summary>
        /// Zoekfunctie om te zoeken in de database.
        /// </summary>
        /// <param name="selected">isbn of stripboektitel </param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<DrukModel> Search(string selected, string search)
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
            }

            using var connection = GetConnection();
            //normal query for list
            IEnumerable<DrukModel> strips =
                connection
                    .Query<DrukModel, StripboekModel, SerieModel, GenreModel, UitgeverModel, DrukModel>(sql,
                        (DrukModel, StripboekModel, SerieModel, GenreModel, UitgeverModel) =>
                        {
                            //vul het main object met de aangemaakte objecten
                            StripboekModel.SerieModel = SerieModel;
                            StripboekModel.GenreModel = GenreModel;
                            DrukModel.StripboekModel = StripboekModel;
                            DrukModel.UitgeverModel = UitgeverModel;
                            return DrukModel;
                        }, new {search}, splitOn: "stripboekId, serieId, genreId, uitgeverId");
            return strips;
        }

        /// <summary>
        /// inserting new druk
        /// </summary>
        /// <param name="stripboekid"></param>
        /// <param name="druknummer"></param>
        /// <param name="drukdatum"></param>
        /// <param name="uitvoering"></param>
        /// <param name="oplage"></param>
        /// <param name="waarde"></param>
        /// <param name="isbn"></param>
        /// <param name="uitgeverid"></param>
        public void insert(int stripboekid, int druknummer, DateTime drukdatum,string uitvoering, int oplage, double waarde, int isbn,int uitgeverid)
        {
            using var connection = GetConnection();
            var sql = @"
                INSERT INTO druk (stripboek_id, druknummer, druk_datum, uitvoering, oplage, waarde, isbn, uitgever_id) 
                VALUES (@stripboekid, @druknummer, @drukdatum, @uitvoering, @oplage, @waarde, @isbn, @uitgeverid );";
            var removeSeparate = connection.Execute(sql, new { stripboekid, druknummer, drukdatum, uitvoering, oplage, waarde, isbn, uitgeverid });
        }


        /// <summary>
        /// updating existing druk
        /// </summary>
        /// <param name="stripboekid"></param>
        /// <param name="druknummer"></param>
        /// <param name="drukdatum"></param>
        /// <param name="uitvoering"></param>
        /// <param name="oplage"></param>
        /// <param name="waarde"></param>
        /// <param name="isbn"></param>
        /// <param name="uitgeverid"></param>
        /// <param name="drukid">the id of the druk you want to edit</param>
        public void update(int stripboekid, int druknummer, DateTime drukdatum, string uitvoering, int oplage, double waarde, int isbn, int uitgeverid, int drukid)
        {
            using var connection = GetConnection();
            var sql =
                @"update druk set stripboek_id = @stripboekid, druknummer = @druknummer, druk_datum = @drukdatum, uitvoering = @uitvoering, oplage = @oplage, waarde = @waarde, isbn = @isbn, uitgever_id = @uitgeverid WHERE drukId = @drukid";
            var removeSeparate = connection.Execute(sql, new { stripboekid, druknummer, drukdatum, uitvoering, oplage, waarde, isbn, uitgeverid, drukid });
        }

        /// <summary>
        /// returns number of druk with a specific isbn
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int count(int isbn)
        {
            string sql = "SELECT COUNT(drukId) FROM druk where isbn=@isbn";
            using var connection = GetConnection();
            int count = connection.ExecuteScalar<int>(sql, new { isbn });
            return count;

        }

        /// <summary>
        /// deletes a single druk from the db based on its id. also deletes associating relational entries
        /// </summary>
        /// <param name="drukid"></param>
        public void DeleteSingle(int drukid)
        {
            WerktAanRepo werktAanRepo = new WerktAanRepo();
            werktAanRepo.DeleteMultiple(drukid);
            //the query
            string sql = @"DELETE FROM druk WHERE drukId = @drukid";
            //the connection
            using var connection = GetConnection();
            //executes query
            connection.Execute(sql, new { drukid });
        }
    }
}