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
                     "INNER JOIN Werkt_aan on Druk.drukId = Werkt_aan.druk_id " +
                     "INNER JOIN Creator on Werkt_aan.creator_id = Creator.creatorId " +
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
                .Query<DrukModel, StripboekModel, SerieModel, GenreModel, WerktAanModel, CreatorModel, UitgeverModel
                    , DrukModel>(sql,
                    (DrukModel, StripboekModel, SerieModel, GenreModel, WerktAanModel, CreatorModel,
                        UitgeverModel) =>
                    {
                        //vul het main object met de aangemaakte objecten
                        StripboekModel.SerieModel = SerieModel;
                        StripboekModel.GenreModel = GenreModel;
                        DrukModel.StripboekModel = StripboekModel;
                        DrukModel.CreatorModel = CreatorModel;
                        DrukModel.WerktAanModel = WerktAanModel;
                        DrukModel.UitgeverModel = UitgeverModel;
                        return DrukModel;
                    }, splitOn: "stripboekId, serieId, genreId, creator_id, creatorId, uitgeverId",
                    commandType: CommandType.Text);
            return strips;
        }

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
                default:
                    sql = sql;
                    break;
            }

            // string sql = "SELECT * " +
            //     "FROM druk " +
            //     "INNER JOIN Stripboek on stripboek.stripboek_id = druk.stripboek_id " +
            //     "INNER JOIN serie on stripboek.serie_id = serie.serie_id " +
            //     "INNER JOIN genre on stripboek.genre_id = genre.genre_id";
            sql = "SELECT * " +
                  "FROM druk " +
                  "INNER JOIN stripboek on stripboek.stripboekId = druk.stripboek_id " +
                  "INNER JOIN serie on serie_id = serie.serieId " +
                  "INNER JOIN genre on genre_id = genre.genreId";



            using var connection = GetConnection();
            //normal query for list
            IEnumerable<DrukModel> strips =
                connection
                    .Query<DrukModel, StripboekModel, SerieModel, GenreModel, WerktAanModel, CreatorModel, UitgeverModel
                        , DrukModel>(sql,
                        (DrukModel, StripboekModel, SerieModel, GenreModel, WerktAanModel, CreatorModel,
                            UitgeverModel) =>
                        {
                            //vul het main object met de aangemaakte objecten
                            StripboekModel.SerieModel = SerieModel;
                            StripboekModel.GenreModel = GenreModel;
                            DrukModel.StripboekModel = StripboekModel;
                            DrukModel.CreatorModel = CreatorModel;
                            DrukModel.WerktAanModel = WerktAanModel;
                            DrukModel.UitgeverModel = UitgeverModel;
                            return DrukModel;
                        }, new {search}, splitOn: "stripboekId, serieId, genreId, creator_id, creatorId, uitgeverId");
            return strips;
        }
    }
}