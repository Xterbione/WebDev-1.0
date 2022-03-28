using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class StripboekKiezen : PageModel
{
    private StripboekRepo stripboekRepo = new StripboekRepo();
    
    public SelectList Stripboeken { get; set; }
    public StripboekModel Stripboek { get; set; }
    
    public void OnGet()
    {
        var stripboeken = stripboekRepo.Get();
        var stripDictionary = stripboeken.ToDictionary(x => x.StripboekId, x => x.stripboektitel);
        Stripboeken = new SelectList(stripDictionary, "Key", "Value");
    }

    public IActionResult OnPost(int stripboekId)
    {
        
        
        return new RedirectToPageResult(nameof(Toevoegen));
    }
}