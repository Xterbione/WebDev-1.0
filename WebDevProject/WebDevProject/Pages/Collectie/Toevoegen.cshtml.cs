using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class Toevoegen : PageModel
{
    // public string errormessage = "";
    
    private WordtOpgeslagenInCollectieRepo wordtOpgeslagenInCollectieRepo = new WordtOpgeslagenInCollectieRepo();
    [BindProperty] public IEnumerable<StripboekModel> Stripboeken { set; get; }
    public IEnumerable<DrukModel> Drukken { set; get; }

    [BindProperty]public WordtOpgeslagenInCollectieVanModel wordtOpgeslagenInCollectieVanModel { get; set; } =
        new WordtOpgeslagenInCollectieVanModel();
    
    
    public void OnGet(int stripboekId)
    {
        Drukken = new DrukRepo().Get(stripboekId);
    }
    
    public IActionResult OnPost()
    {
        wordtOpgeslagenInCollectieVanModel.gebruiker_id = GetUserId();
        new WordtOpgeslagenInCollectieRepo().Add(wordtOpgeslagenInCollectieVanModel);
        DrukRepo drukRepo = new DrukRepo();
        var druk = drukRepo.GetByDrukId(wordtOpgeslagenInCollectieVanModel.druk_id);
        var drukModel = druk.First();
        string succes = $"Je hebt het boek {drukModel.StripboekModel.stripboektitel} met druknummer {drukModel.druknummer} toegevoegd";
        return RedirectToPage(nameof(Collectie.Index), new { succes = succes, isToegevoegd = true});

    }

    public int GetUserId()
    {
        if (HttpContext.Session.GetString("cockie") != "" )
        {
            GebruikerModel gebruiker = new GebruikerModel();
            gebruiker = JsonSerializer.Deserialize<GebruikerModel>(HttpContext.Session.GetString("cockie"));
            return gebruiker.gebruikerId;
        }

        return 0;
    }
}