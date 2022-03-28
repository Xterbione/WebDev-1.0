using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class uitgeverbeheer : PageModel
{
    public IEnumerable<UitgeverModel> uitgevers { get; set; }
    private UitgeverRepo uitgeverRepo = new UitgeverRepo();
    [BindProperty(SupportsGet = true)] public string uitgever { get; set; }  
    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("cockie") == null)
        {
            return new RedirectToPageResult("/Index");

        }
       
        else {
            uitgevers = uitgeverRepo.Get();
        }
        return Page();
    }
    public void OnPostDelete([FromForm] string? hiddenUitgever)
    {
        uitgeverRepo.DeleteSingle(hiddenUitgever);
        uitgevers = uitgeverRepo.Get();

    }
    public void OnPostAdd()
    {
        if (uitgever!= null)
        {
            uitgeverRepo.insert(uitgever);
            uitgevers = uitgeverRepo.Get();
        }
    }
}