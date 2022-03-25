using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class Index : PageModel
{
    public IEnumerable<WordtOpgeslagenInCollectieVanModel> WordtOpgeslagenInCollectieVanModels { get; set; }

    public WordtOpgeslagenInCollectieRepo WordtOpgeslagenInCollectieRepo = new();
    public DrukRepo drukRepo = new ();
    
    public GebruikerModel Gebruiker { get; set; }
    public WordtOpgeslagenInCollectieVanModel WordtOpgeslagenInCollectieVanModel { get; set; }
    
    
    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("cockie") != null)
        {
            string sessionstring = HttpContext.Session.GetString("cockie");
            Gebruiker = JsonConvert.DeserializeObject<GebruikerModel>(sessionstring);

            WordtOpgeslagenInCollectieVanModels = WordtOpgeslagenInCollectieRepo.Get(Gebruiker.GebruikerId);
            
            return Page();
        }
        return new RedirectToPageResult("/Login");

    }
}