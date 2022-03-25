using System.Data;
using Dapper;
using NuGet.Protocol.Plugins;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories;

public class WerktAanRepo
{
    private static IDbConnection GetConnection()
    {
        return new DBUtils().GetDbConnection();
    }


    /// <summary>
    /// gets al genres in a ienumerable list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<WerktAanModel> Get()
    {
        string sql = "SELECT * " +
                     "FROM Werkt_aan " +
                     "INNER JOIN Creator on Creator.creatorId = Werkt_aan.creator_id " +
                     "INNER JOIN Druk on druk.drukId = Werkt_aan.druk_id "
                     ;
         

        using var connection = GetConnection();
        //normal query for list
        var strips = connection.Query<WerktAanModel>(sql);
        return strips;
    }
    
    public IEnumerable<WerktAanModel> Get(int drukId)
    {
        string sql = @"SELECT * 
                     FROM Werkt_aan 
                     INNER JOIN Creator on Creator.creatorId = Werkt_aan.creator_id 
                     WHERE druk_id = @drukId;";


        using var connection = GetConnection();
        //normal query for list
        var strips = connection.Query<WerktAanModel, CreatorModel, WerktAanModel>(sql, (WerktAanModel, CreatorModel) =>
        {
            WerktAanModel.CreatorModel = CreatorModel;
            return WerktAanModel;
        },new {drukId}, splitOn: "CreatorId" );
        return strips;
    }
    /// <summary>
    /// deletes a single entry based on input
    /// </summary>
    /// <param name="drukid"></param>
    /// <param name="creatorid"></param>
    /// <param name="rol"></param>
    public void DeleteSingle(int drukid, int creatorid, string rol)
    {
        //the query
        string sql = @"DELETE FROM werkt_aan WHERE creator_id = @creatorid AND druk_id = @drukid AND rol = @rol";
        //the connection
        using var connection = GetConnection();
        //executes query
        connection.Execute(sql, new { creatorid, drukid, rol });
    }
    /// <summary>
    /// inserting entry for werkt_aan
    /// </summary>
    /// <param name="drukid"></param>
    /// <param name="creatorid"></param>
    /// <param name="rol"></param>
    public void insert(int drukid, int creatorid, string rol)
    {
        using var connection = GetConnection();
        var sql = @"
                INSERT INTO werkt_aan (creator_id, druk_id, rol) 
                VALUES (@creatorid, @drukid, @rol );";
        var removeSeparate = connection.Execute(sql, new { creatorid, drukid, rol });
    }

    /// <summary>
    /// counts entries and gives the number back
    /// </summary>
    /// <param name="drukid"></param>
    /// <param name="creatorid"></param>
    /// <param name="rol"></param>
    /// <returns></returns>
    public int count(int drukid, int creatorid, string rol)
    {
        string sql = "SELECT COUNT(druk_id) FROM werkt_aan where creator_id = @creatorid AND druk_id = @drukid AND rol = @rol";
        using var connection = GetConnection();
        int count = connection.ExecuteScalar<int>(sql, new { creatorid, drukid, rol });
        return count;

    }
    /// <summary>
    /// deletes multiple entries based on id
    /// </summary>
    /// <param name="drukid"></param>
    public void DeleteMultiple(int drukid)
    {
        //the query
        string sql = @"DELETE FROM werkt_aan WHERE druk_id = @drukid";
        //the connection
        using var connection = GetConnection();
        //executes query
        connection.Execute(sql, new { drukid });
    }
}