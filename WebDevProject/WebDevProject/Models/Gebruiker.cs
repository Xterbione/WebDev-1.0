namespace WebDevProject.Models;

public class Gebruiker
{
    private int gebruiker_id;
    
    private string gebruikersnaam;

    private DateOnly geboortedatum;

    private string email;
    
    
    /// <summary>
    /// geeft aantal boeken van gebruiker terug
    /// </summary>
    /// <returns></returns>
    public int AantalBoeken()
    {
        return 0;
    }

    /// <summary>
    /// geeft aantal series van gebruiker terug
    /// </summary>
    /// <returns></returns>
    public int AantalSeries()
    {
        return 0;
    }
}