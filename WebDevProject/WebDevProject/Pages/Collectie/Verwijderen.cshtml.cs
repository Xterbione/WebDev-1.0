using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages.Collectie;

public class Verwijderen : PageModel
{
    public WordtOpgeslagenInCollectieVanModel WordtOpgeslagenInCollectieVanModel { get; set; }
    public IEnumerable<WordtOpgeslagenInCollectieVanModel> collectieModels { get; set; }
    public int DrukId { set; get; }
    public void OnGet([FromRoute] int drukId, [FromRoute] int gebruikerId)
    {
        DrukId = drukId;
        collectieModels = new WordtOpgeslagenInCollectieRepo().Get(gebruikerId);
    }

    public IActionResult OnPostDelete([FromRoute]int drukId, [FromRoute] int gebruikerId)
    {
        bool success = new WordtOpgeslagenInCollectieRepo().Delete(gebruikerId, drukId);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}