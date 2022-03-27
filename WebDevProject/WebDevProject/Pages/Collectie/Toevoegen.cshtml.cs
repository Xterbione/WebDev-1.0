using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class Toevoegen : PageModel
{
    private int GebruikerId { get; set; }
    
    public GebruikerModel gebruiker { get; set; }
    public void OnGet([FromRoute] int gebuikerId)
    {
        GebruikerId = gebuikerId;
    }
}