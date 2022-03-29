using System.Data;
using Dapper;
using WebDevProject.Models;

using WebDevProject.Pages;



namespace WebDevProject.Repositories;

public class GebruikerRepo
{
    private static IDbConnection GetConnection()
    {
        return new DBUtils().GetDbConnection();
    }
    
    /// <summary>
    /// gets al Gebruikers in a ienumerable list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<GebruikerModel> Get()
    {
        string sql = "SELECT * " +
                     "FROM Gebruiker";


        using var connection = GetConnection();
        //normal query for list
        var gebruiker = connection.Query<GebruikerModel>(sql);
        return gebruiker;
    }

    /// <summary>
    /// user registeration 
    /// </summary> 
    /// <param name="naam"></param>
    /// <param name="geboortedatum"></param>
    /// <param name="email"></param>
    public void Register(string gebruikersnaam, string email,DateTime geboortedatum, string wachtwoord)
    {
        var connection = GetConnection();
        string sql = @"INSERT INTO gebruiker(gebruikersnaam, geboortedatum, email, wachtwoord)
                       VALUES (@gebruikersnaam,@geboortedatum, @email,@wachtwoord)";
        var excute = connection.Execute(sql, new {gebruikersnaam, geboortedatum,email, wachtwoord});
    }

    public GebruikerModel Get(string email)
     {
         string sql = "SELECT * FROM gebruiker WHERE email= @email";

         using var connection = GetConnection();
         var gebruiker = connection.QuerySingle<GebruikerModel>(sql, new { email });
         return gebruiker;
     }
    
    /// <summary>
    /// Gets gebruikerModel from gebruikerId
    /// </summary>
    /// <param name="gebruikerId"></param>
    /// <returns></returns>
    public GebruikerModel Get(int gebruikerId)
    {
        string sql = "SELECT * FROM gebruiker WHERE gebruikerId= @gebruikerId";

        using var connection = GetConnection();
        var gebruiker = connection.QuerySingle<GebruikerModel>(sql, new { gebruikerId });
        return gebruiker;
    }

    public int count(string email)
    {
        string sql = "SELECT COUNT(gebruikerId) FROM gebruiker where email=@email";
        using var connection = GetConnection();
        int count = connection.ExecuteScalar<int>(sql, new {email});
        return count;

    }

}