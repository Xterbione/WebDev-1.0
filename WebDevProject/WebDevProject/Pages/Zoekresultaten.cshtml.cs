using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class Zoekresultaten : PageModel
{
    [BindProperty(SupportsGet = true)] public string Search { get; set; }
    [BindProperty(SupportsGet = true)] public string Option { get; set; }
    public IEnumerable<StripboekModel> StripboekModels { get; set; }
    public StripBoekRepo stripboekRepo = new();
    
    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        StripboekModels = stripboekRepo.Get();
        if (Search != "" && Option != "")
        {
            StripboekModels = stripboekRepo.Search(Option, Search);
        }
        
    }
}