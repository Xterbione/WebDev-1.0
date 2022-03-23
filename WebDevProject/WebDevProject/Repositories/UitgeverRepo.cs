using System.Data;
using Dapper;
using WebDevProject.Models;
using WebDevProject.Pages;

namespace WebDevProject.Repositories;

public class UitgeverRepo
{
    private static IDbConnection GetConnection()
    {
        return new DBUtils().GetDbConnection();
    }


    /// <summary>
    /// gets al genres in a ienumerable list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<UitgeverModel> Get()
    {
        string sql = "SELECT * " +
                     "FROM Uitgever ";


        using var connection = GetConnection();
        //normal query for list
        var strips = connection.Query<UitgeverModel>(sql);
        return strips;
    }

}