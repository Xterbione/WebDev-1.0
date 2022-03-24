using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class Zoekresultaten : PageModel
{
    [BindProperty(SupportsGet = true)] public string Search { get; set; }
    [BindProperty(SupportsGet = true)] public string Option { get; set; }

    public IEnumerable<DrukModel> DrukModels { get; set; } 
    public DrukRepo drukRepro = new();

    public void OnGet()
    {
        Search = Search.ToLower();
        DrukModels = drukRepro.Get();
        if (Search != "" && Option != "")
        {
            DrukModels = drukRepro.Search(Option, Search);
        }
    }
}
    