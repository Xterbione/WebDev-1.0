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

}