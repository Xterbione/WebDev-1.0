namespace WebDevProject.Models;

public class GebruikerModel
{
    public int GebruikerId { get; set; }
    public string gebruikersnaam { get; set; }
    public DateTime geboortedatum { get; set; }
    public string email { get; set; }
    public string wachtwoord { set; get; }
}