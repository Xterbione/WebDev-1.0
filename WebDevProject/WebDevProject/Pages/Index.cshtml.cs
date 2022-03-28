using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class Index : PageModel
{
    private readonly ILogger<Index> _logger;
    public IEnumerable<StripboekModel> StripboekModels { get; set; }
    public StripBoekRepo stripboekRepo = new();
    

    public Index(ILogger<Index> logger)
    {
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)] public string Search { get; set; }
    [BindProperty(SupportsGet = true)] public string Option { get; set; }

    public void OnGet()
    {
        StripboekModels = stripboekRepo.Get();

        if (Search != "" && Option != "")
        {
            StripboekModels = stripboekRepo.Search(Option, Search);
        }
    }
}