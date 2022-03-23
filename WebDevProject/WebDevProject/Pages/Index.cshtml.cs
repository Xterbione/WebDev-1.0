using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDevProject.Models;
using WebDevProject.Repositories;

namespace WebDevProject.Pages;

public class IndexModel : PageModel
{
    public IEnumerable<StripboekModel> StripboekModels { get; set; }
    private readonly ILogger<IndexModel> _logger;
    public StripBoekRepo stripboekRepo = new();

    public IndexModel(ILogger<IndexModel> logger)
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