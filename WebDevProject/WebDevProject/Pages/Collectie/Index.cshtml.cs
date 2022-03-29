using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using  System.Text.Json;
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
    public List<string> series = new List<string>();

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("cockie") != null)
        {
            string sessionstring = HttpContext.Session.GetString("cockie");
            Gebruiker = JsonSerializer.Deserialize<GebruikerModel>(sessionstring);

            WordtOpgeslagenInCollectieVanModels = WordtOpgeslagenInCollectieRepo.Get(Gebruiker.gebruikerId);
            foreach (var item in WordtOpgeslagenInCollectieVanModels)
            {
                string serie = item.DrukModel.StripboekModel.SerieModel.serieTitel;
                if (!series.Contains(serie))
                {
                    series.Add(serie);
                }
            }
            return Page();
        }
        return new RedirectToPageResult("/Login");

    }
}