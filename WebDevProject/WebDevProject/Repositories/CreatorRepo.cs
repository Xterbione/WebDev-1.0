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
    public void DeleteSingle(string? creatorUitList)
    {
        //the query
        string sql = @"DELETE FROM creator WHERE creator_naam = @creatorUitList";
        //the connection
        using var connection = GetConnection();
        //executes query
        connection.Execute(sql, new {creatorUitList});
    }
    public void insert(string creator)
    {
        using var connection = GetConnection();
        var sql = @"
                INSERT INTO creator (creator_naam) 
                VALUES (@creator)";
        var removeSeparate = connection.Execute(sql, new {creator});
    }
    
    public void Update(int creatorId, string creator)
    {
        using var connection = GetConnection();
        var sql = @"
                UPDATE creator SET creator_naam=@creator WHERE creatorId=@creatorId";
        var removeSeparate = connection.Execute(sql, new {creator, creatorId});
    }

}