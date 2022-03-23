using System.Data;
using Dapper;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories;

public class CreatorRepo
{
    private static IDbConnection GetConnection()
    {
        return new DBUtils().GetDbConnection();
    }


    /// <summary>
    /// gets al creators in a ienumerable list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<CreatorModel> Get()
    {
        string sql = "SELECT * " +
                     "FROM Creator ";


        using var connection = GetConnection();
        //normal query for list
        var strips = connection.Query<CreatorModel>(sql);
        return strips;
    }

}