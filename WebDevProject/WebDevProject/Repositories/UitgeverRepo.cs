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
    
    public void DeleteSingle(string? uitgeverUitList)
    {
        //the query
        string sql = @"DELETE FROM uitgever WHERE uitgever_naam = @uitgeverUitList";
        //the connection
        using var connection = GetConnection();
        //executes query
        connection.Execute(sql, new {uitgeverUitList});
    }
    public void insert(string uitgever)
    {
        using var connection = GetConnection();
        var sql = @"
                INSERT INTO uitgever (uitgever_naam) 
                VALUES (@uitgever)";
        var removeSeparate = connection.Execute(sql, new {uitgever});
    }
    public void Update(int uitgeverId, string uitgever)
    {
        using var connection = GetConnection();
        var sql = @"
                UPDATE uitgever SET uitgever_naam=@uitgever WHERE uitgeverId=@uitgeverId";
        var removeSeparate = connection.Execute(sql, new {uitgever, uitgeverId});
    }

}