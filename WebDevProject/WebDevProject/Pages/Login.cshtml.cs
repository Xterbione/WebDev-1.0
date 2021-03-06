using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class Login : PageModel
{
    [BindProperty, DataType(DataType.EmailAddress)]
    public string email { get; set; }

    [BindProperty, DataType(DataType.Password)]
    public string wachtwoord { get; set; }

    public string Message { get; set; }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("cockie") != null)
        {
            HttpContext.Session.Remove("cockie");
            Message = "Logout succesful";
           // return new RedirectToPageResult("/Index");

        }
       return Page();
    }

    public IActionResult OnPost()
    {
        if (email != null && wachtwoord != null)
        {
            var userRepo = new GebruikerRepo();
            GebruikerModel user = userRepo.Get(email);
            if (user != null)
            {
                if (user.wachtwoord == wachtwoord )
                {
                    var cockie = JsonSerializer.Serialize<GebruikerModel>(user);
                    HttpContext.Session.SetString("cockie",cockie.ToString());
                    Message = "Login Succesful";
                    return new RedirectToPageResult("/collectie/index");
                }

                Message = "Wachtwoord is niet juist";
                return Page();

            }

            Message = "Inloggen mislukt";
            return Page();

        }
        return Page();


    }
}