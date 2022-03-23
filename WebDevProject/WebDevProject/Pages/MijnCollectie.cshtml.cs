using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class MijnCollectie : PageModel
{
    public IEnumerable<WordtOpgeslagenInCollectieVanModel> WordtOpgeslagenInCollectieVanModels { get; set; }

    public WordtOpgeslagenInCollectieRepo repo = new();
    public void OnGet()
    {
        WordtOpgeslagenInCollectieVanModels = repo.Get(1);
    }
}