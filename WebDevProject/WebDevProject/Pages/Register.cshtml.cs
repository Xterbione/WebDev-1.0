using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Security;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class Register : PageModel
{
    
    [BindProperty][Required] public string gebruikersnaam { get; set; }
    
    [BindProperty, DataType(DataType.EmailAddress)][Required] public string email{ get; set; }
    
    [BindProperty] [Required]public DateTime geboortedatum { get; set; }
    
    [BindProperty, DataType(DataType.Password)][Required] public string wachtwoord { get; set; }

    public string Message { get; set; }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("cockie") != null)
        {
            return new RedirectToPageResult("/Index");

        }
        return Page();
    }

    public void OnPost()
    {
        if (gebruikersnaam !=null && email !=null && geboortedatum != null && wachtwoord !=null )
        {
            var register = new GebruikerRepo();
            int count = register.count(email);
            if (count>0)
            {
                Message = "Dit email adres wordt gebruikt door iemand anders!";
            }

            else
            {
                register.Register(gebruikersnaam, email, geboortedatum, wachtwoord);
                Message = "je bent nu geregistreerd, je kunt nu inloggen";
            }

          
        }

        else
        {
            Message = "registratie mislukt, probeer het opnieuw";
        }

    }

}