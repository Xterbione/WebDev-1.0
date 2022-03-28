using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class Toevoegen : PageModel
{
    public string erromessage = "";
    
    private WordtOpgeslagenInCollectieRepo wordtOpgeslagenInCollectieRepo = new WordtOpgeslagenInCollectieRepo();
    private DrukRepo DrukRepo = new();
    private StripboekRepo stripboekRepo = new StripboekRepo();
    public int GebruikerId { get; set; }
    public SelectList Stripboeken { get; set; }
    public SelectList Drukken { get; set; }
    public GebruikerModel Gebruiker { get; set; }

    [BindProperty] public WordtOpgeslagenInCollectieVanModel NieuwBoek { get; set; }
    
    public void OnGet(int gebruikerId)
    {
        GebruikerId = gebruikerId;
        var stripboeken = stripboekRepo.Get();
        var stripDictionary = stripboeken.ToDictionary(x => x.StripboekId, x => x.stripboektitel);
        Stripboeken = new SelectList(stripDictionary, "Key", "Value");

        if (HttpContext.Session.GetString("cockie") != null)
        {
            string sessionstring = HttpContext.Session.GetString("cockie");
            Gebruiker = JsonConvert.DeserializeObject<GebruikerModel>(sessionstring);
        }
    }

    public void OnPostStripboek(int stripboekId)
    {
        var drukModels = DrukRepo.Get(stripboekId);
        
        var drukDictionary = drukModels.ToDictionary(x => x.drukId, x => x.druk_datum );
        Drukken = new SelectList(drukDictionary, "Key", "Value");
        
    }

    public IActionResult OnPostDruk()
    {
        if (HttpContext.Session.GetString("cockie") != null)
        {
            string sessionstring = HttpContext.Session.GetString("cockie");
            Gebruiker = JsonConvert.DeserializeObject<GebruikerModel>(sessionstring);
        }
        
        wordtOpgeslagenInCollectieRepo.Add(NieuwBoek);
        return new RedirectToPageResult(nameof(Collectie.Index));
    }
}