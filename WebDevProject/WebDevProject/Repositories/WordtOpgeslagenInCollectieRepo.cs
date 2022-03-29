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

    public string sql = @"SELECT * " +
                         "FROM Wordt_opgeslagen_in_collectie_van " +
                         "INNER JOIN Druk on Wordt_opgeslagen_in_collectie_van.druk_id = Druk.drukId " +
                         "INNER JOIN Stripboek on stripboek.stripboekId = druk.stripboek_id " +
                         "INNER JOIN Uitgever on Druk.uitgever_id = Uitgever.uitgeverId " +
                         "INNER JOIN serie on stripboek.serie_id = serie.serieId " +
                         "INNER JOIN genre on stripboek.genre_id = genre.genreId ";
    /// <summary>
    /// gets all books
    /// </summary>
    /// <returns></returns>
    public IEnumerable<WordtOpgeslagenInCollectieVanModel> Get(int gebruikerId)
    {
        
            sql = sql + "WHERE Wordt_opgeslagen_in_collectie_van.gebruiker_id = @gebruikerId ";

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
    
    /// <summary>
    /// Verwijdert een rij uit de database met de waarden van gebruikerId en DrukId
    /// </summary>
    /// <param name="gebruikerId"></param>
    /// <param name="drukId"></param>
    /// <returns></returns>
    public bool Delete(int gebruikerId, int drukId)
    {
        string sql = @"DELETE FROM Wordt_opgeslagen_in_collectie_van
                    WHERE druk_id = @drukId AND gebruiker_id = @gebruikerId";
            
        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { gebruikerId, drukId });
        return numOfEffectedRows == 1;
    }

    /// <summary>
    /// gebruiker_id wordt niet goed opgenomen.
    /// </summary>
    /// <param name="druk_id"></param>
    /// <param name="gebruiker_id">Deze wordt niet opgenomen</param>
    /// <param name="staat"></param>
    /// <param name="aankoop_waarde"></param>
    /// <param name="aankoop_locatie"></param>
    /// <param name="beoordeling"></param>
    /// <param name="opslag_locatie"></param>
    public void Add(int druk_id, int gebruiker_id, string staat, double aankoop_waarde, string aankoop_locatie,
        int beoordeling, string opslag_locatie)
    {
        var connection = GetConnection();
        string sql =
            @"INSERT INTO Wordt_opgeslagen_in_collectie_van(druk_id, gebruiker_id, staat, aankoop_waarde, aankoop_locatie, beoordeling, opslag_locatie)
                       VALUES (@druk_id, @gebruiker_id, @staat, @aankoop_waarde, @aankoop_locatie, @beoordeling, @opslag_locatie)";
        var excute = connection.Execute(sql,
            new {druk_id, gebruiker_id, staat, aankoop_waarde, aankoop_locatie, beoordeling, opslag_locatie});
    }

    public void Add(WordtOpgeslagenInCollectieVanModel nieuwBoek)
    {
        var connection = GetConnection();
        string sql =
            @"INSERT INTO Wordt_opgeslagen_in_collectie_van(druk_id, gebruiker_id, staat, aankoop_waarde, aankoop_locatie, beoordeling, opslag_locatie)
                       VALUES (@druk_id, @gebruiker_id, @staat, @aankoop_waarde, @aankoop_locatie, @beoordeling, @opslag_locatie)";
        var excute = connection.Execute(sql, nieuwBoek);
    }

    public void DeleteSingle(int drukId)
    {
        //the query
        string sql = @"DELETE FROM Wordt_opgeslagen_in_collectie_van WHERE druk_id = @drukId";
        //the connection
        using var connection = GetConnection();
        //executes query
        connection.Execute(sql, new {drukId});
    }
}