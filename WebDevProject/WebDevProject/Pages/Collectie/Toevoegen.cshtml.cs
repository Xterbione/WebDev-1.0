using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class Toevoegen : PageModel
{
    public string erromessage = "";
    private WordtOpgeslagenInCollectieRepo wordtOpgeslagenInCollectieRepo = new WordtOpgeslagenInCollectieRepo();
    private DrukRepo DrukRepo = new();
    public SelectList Drukken { get; set; }


    [BindProperty] public WordtOpgeslagenInCollectieVanModel NieuwBoek { get; set; }
    
    public void OnGet([FromRoute] int gebuikerId)
    {
        NieuwBoek.gebruiker_id = gebuikerId;
        var drukModels = DrukRepo.Get();
        
        var drukDictionary = drukModels.ToDictionary(x => x.drukId, x => x.StripboekModel.stripboektitel);
        Drukken = new SelectList(drukDictionary, "Key", "Value");
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            erromessage = "Stripboeken is niet gelukt, probeer het opnieuw";
            return Page();
        }

        var newBook = wordtOpgeslagenInCollectieRepo.Insert(NieuwBoek);
        return new RedirectToPageResult(nameof(Collectie.Index));
    }
}