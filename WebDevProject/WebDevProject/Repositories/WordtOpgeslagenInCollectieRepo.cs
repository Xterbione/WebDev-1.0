using System.Data;
using Dapper;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories;

public class WordtOpgeslagenInCollectieRepo
{
    private static IDbConnection GetConnection()
    {
        return new DBUtils().GetDbConnection();
    }

    /// <summary>
    /// gets all books
    /// </summary>
    /// <returns></returns>
    public IEnumerable<WordtOpgeslagenInCollectieVanModel> Get(int gebruikerId)
    {
        string sql = @"SELECT * " +
                     "FROM Wordt_opgeslagen_in_collectie_van " +
                     "INNER JOIN Druk on Wordt_opgeslagen_in_collectie_van.druk_id = Druk.drukId " +
                     "INNER JOIN Stripboek on stripboek.stripboekId = druk.stripboek_id " +
                     "INNER JOIN Uitgever on Druk.uitgever_id = Uitgever.uitgeverId " +
                     "INNER JOIN serie on stripboek.serie_id = serie.serieId " +
                     "INNER JOIN genre on stripboek.genre_id = genre.genreId " +
                     "WHERE Wordt_opgeslagen_in_collectie_van.gebruiker_id = @gebruikerId ";

        using var connection = GetConnection();
        //normal query for list
        IEnumerable<WordtOpgeslagenInCollectieVanModel> strips =
            connection
                .Query<WordtOpgeslagenInCollectieVanModel, DrukModel, StripboekModel, UitgeverModel, SerieModel,
                    GenreModel, WordtOpgeslagenInCollectieVanModel>(
                    sql, (WordtOpgeslagenInCollectieVanModel, DrukModel, StripboekModel, UitgeverModel, SerieModel,
                        GenreModel) =>
                    {
                        //vul het main object met de aangemaakte objecten
                        StripboekModel.SerieModel = SerieModel;
                        StripboekModel.GenreModel = GenreModel;
                        DrukModel.UitgeverModel = UitgeverModel;
                        DrukModel.StripboekModel = StripboekModel;
                        WordtOpgeslagenInCollectieVanModel.DrukModel = DrukModel;
                        return WordtOpgeslagenInCollectieVanModel;
                    }, new {gebruikerId}, splitOn: "drukId, stripboekId, uitgeverId, serieId, genreId");
        return strips;
    }

    public void Add(WordtOpgeslagenInCollectieVanModel nieuwBoek)
    {
        var connection = GetConnection();
        string sql = @"INSERT INTO Wordt_opgeslagen_in_collectie_van(druk_id, gebruiker_id, staat, aankoop_waarde, aankoop_locatie, beoordeling, opslag_locatie)
                       VALUES (@nieuwBoek.druk_id, @nieuwBoek.gebruiker_id, @nieuwBoek.staat, @nieuwBoek.aankoop_waarde, @nieuwBoek.aankoop_locatie, @nieuwBoek.beoordeling, @nieuwBoek.opslag_locatie)";
        var excute = connection.Execute(sql, new {nieuwBoek});
    }

    /// <summary>
    /// Inserts new boek in collectie van gebruiker
    /// </summary>
    /// <param name="nieuwBoek"></param>
//     public WordtOpgeslagenInCollectieVanModel Insert(WordtOpgeslagenInCollectieVanModel nieuwBoek)
//     {
//         using var connection = GetConnection();
//         var sql = @"INSERT INTO Wordt_opgeslagen_in_collectie_van (druk_id, gebruiker_id, staat, aankoop_waarde, aankoop_locatie, beoordeling, opslag_locatie)
//                     VALUES (@nieuwBoek.druk_id, @nieuwBoek.gebruiker_id, @nieuwBoek.staat, @nieuwBoek.aankoop_waarde, @nieuwBoek.aankoop_locatie, @nieuwBoek.beoordeling, @nieuwBoek.opslag_locatie );";
//         var removeSeparate = connection.Execute(sql, new {nieuwBoek});
//         return nieuwBoek;
//     }
}